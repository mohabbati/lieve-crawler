namespace Lieve.Crawler.Application.Implementations.Alibaba.Resources;

public static class Resources
{
    public static List<string> GetAirportsIataCodes()
    {
        var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Implementations/Alibaba/Resources/IataCodes.txt");
        var lines = File.ReadAllLines(filePath);

        return [..lines];
    }
}