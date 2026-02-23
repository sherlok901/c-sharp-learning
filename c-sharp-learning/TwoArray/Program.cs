class Program
{
    static int _count = 0;

    static void Main(string[] args)
    {
       
    }
    
    double multiply_arrays(
        List<int> a_indices, List<double> a_values,
        List<int> b_indices, List<double> b_values)
    {
        int i = 0, j = 0;
        double result = 0;
    
        while (i < a_indices.Count && j < b_indices.Count)
        {
            int aIndex = a_indices[i];
            int bIndex = b_indices[j];
    
            if (aIndex == bIndex)
            {
                // Обчислюємо середнє для a
                double aSum = 0;
                int aCount = 0;
                while (i < a_indices.Count && a_indices[i] == aIndex)
                {
                    aSum += a_values[i];
                    aCount++;
                    i++;
                }
                double aAvg = aSum / aCount;
    
                // Обчислюємо середнє для b
                double bSum = 0;
                int bCount = 0;
                while (j < b_indices.Count && b_indices[j] == bIndex)
                {
                    bSum += b_values[j];
                    bCount++;
                    j++;
                }
                double bAvg = bSum / bCount;
    
                result += aAvg * bAvg;
            }
            else if (aIndex < bIndex)
            {
                // пропускаємо всі дублікати a
                while (i < a_indices.Count && a_indices[i] == aIndex)
                    i++;
            }
            else
            {
                // пропускаємо всі дублікати b
                while (j < b_indices.Count && b_indices[j] == bIndex)
                    j++;
            }
        }
    
        return result;
    }
}

