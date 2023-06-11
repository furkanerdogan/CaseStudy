
List<string> uniqecode = new List<string>();

    string temp = GenerateCode();

    if (!uniqecode.Contains(temp))
      uniqecode.Add(temp);

    Console.WriteLine(temp);

 string GenerateCode()
{
    char[] constraint = { 'A', 'C', 'D', 'E', 'F', 'G', 'H', 'K', 'L', 'M', 'N', 'P', 'R', 'T', 'X', 'Y', 'Z', '2', '3', '4', '5', '7', '9' };
    int codeLength = 8;
    Random rnd = new Random();
    string generatedCode = "";

    for (int i = 0; i < codeLength; i++)
    {
        int randomNumber = rnd.Next(1, constraint.Length);
        generatedCode += constraint[randomNumber];
    }
    return generatedCode;
}

