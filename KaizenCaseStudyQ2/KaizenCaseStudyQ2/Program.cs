

using Newtonsoft.Json.Linq;

JsonParse();
void JsonParse()
{
    // Read the JSON file
    string filePath = @"C:\Users\furka\source\repos\KaizenCaseStudy\KaizenCaseStudy\response.json";
    string json = File.ReadAllText(filePath);
    // Parse the JSON array
    JArray jsonArray = JArray.Parse(json);

    //I Deleted first element cuz first element
    jsonArray.RemoveAt(0);

    Dictionary<(int x, int y), List<string>> positions = new Dictionary<(int x, int y), List<string>>();

    foreach (JObject jsonObject in jsonArray)
    {
        string description = jsonObject["description"].ToString();
        JArray vertices = (JArray)jsonObject["boundingPoly"]["vertices"];
        int minX = int.MaxValue;
        int minY = int.MaxValue;

        foreach (JObject vertex in vertices)
        {
            int x = int.Parse(vertex["x"].ToString());
            int y = int.Parse(vertex["y"].ToString());
            minX = Math.Min(minX, x);
            minY = Math.Min(minY, y);
        }
        var position = (x: minX, y: minY);//30,80
        if (positions.ContainsKey(position))
        {
            positions[position].Add(description);
        }
        else
        {
            positions[position] = new List<string> { description };
        }
    }

    List<(int x, int y)> sortedPositions = positions.Keys.OrderBy(pos => pos.y).ThenBy(pos => pos.x).ToList();
    List<List<string>> groupedDescriptions = new List<List<string>>();
    List<string> currentGroup = new List<string>();
    int prevY = sortedPositions.ElementAt(0).y;

    int range = 10; // aynı y ekseni üzerindeki sözcüklerin eksen üzerindeki değişme ortalaması
    foreach (var position in sortedPositions)
    {
        var descriptions = positions[position];


        if (position.y - prevY > range)//aynı satırda  değilse
        {

            groupedDescriptions.Add(currentGroup);

            currentGroup = new List<string>();//reset process
        }
        currentGroup.AddRange(descriptions);
        prevY = position.y;

    }
    groupedDescriptions.Add(currentGroup);
    foreach (var group in groupedDescriptions)
    {
        foreach (string description in group)
        {
            Console.Write(description + " ");
        }

        Console.WriteLine();
    }

}
