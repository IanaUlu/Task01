///
/// Name: minSplit          
/// Date: 08.01.2021         
/// Description: Returns the minimum number of coins for input amount
///

static string minSplit(int amount)
{
    string inpAmout = amount.ToString();

    // array with exist nominals
    int[] nominals = { 1, 5, 10, 20, 50 };

    List<int> monets = new List<int>();
    //
    int nominalsAll = nominals.Length;

    for (int i = nominalsAll - 1; i >= 0; i--)
    {
        while (amount >= nominals[i])
        {
            amount = amount - nominals[i];
            monets.Add(nominals[i]);
        }
    }

    return("Input amount: " + inpAmout + " Nominals:" + "[{0}]", string.Join(", ", monets));
}




