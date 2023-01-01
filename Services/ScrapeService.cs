using System;
using System.Linq;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using scraperor_v2.Dtos;

namespace scraperor_v2.Services;

public interface IScrapeService
{
    Task<string[]> ScrapeAsync(string url, ScrapeBodyPointerDto pointer);
}

public class ScrapeService : IScrapeService
{
    private readonly HttpClient _httpClient;

    public ScrapeService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string[]> ScrapeAsync(string url, ScrapeBodyPointerDto pointer)
    {
        var response = await _httpClient.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();

        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(content);

        List<ScrapeBodyPointerLookForDto> steps = new List<ScrapeBodyPointerLookForDto>();
        ScrapeBodyPointerLookForDto? currentLookFor = pointer.LookFor;
        do
        {
            steps.Add(currentLookFor!);
            currentLookFor = currentLookFor?.ThenLookFor;
        }
        while (currentLookFor != null);

        dynamic virtualTraversal = doc.DocumentNode.AncestorsAndSelf();

        foreach (var step in steps)
        {
            if (step.Tag != null)
            {
                virtualTraversal = ((IEnumerable<HtmlNode>)virtualTraversal).SelectMany(x => x.Descendants(step.Tag));
            }

            if (step.HasClasses != null && step.HasClasses.Length > 0)
            {

                foreach(string className in step.HasClasses)
                {
                    virtualTraversal = ((IEnumerable<HtmlNode>)virtualTraversal).SelectMany(x => x.AncestorsAndSelf().Where(z => z.HasClass(className)));
                }
            }
        }


        var gatheredElement = from v in virtualTraversal as IEnumerable<HtmlNode>
                        select v.InnerText;


        return gatheredElement.ToArray();
    }
}

