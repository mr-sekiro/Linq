using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Text.RegularExpressions;
using System.Security.Claims;
using System.Xml.Linq;
using static Linq.ListGenerator;
using Linq.Data;
using System.Diagnostics;

namespace Linq
{

    //Stand For => Language Integrated Query
    //LINQ Method +40 Method
    //Extension Method For All Collection That Implement Interface "IEnumerable"
    //Named As LINQ Operators Existed at Class Enumerable
    //Categorized Into 13 Category
    //You Can Use "LINq Operators" Against The Data[Stored In Sequence] Regardless Data Store[Sql Server, MySql, Oracle]

    //Sequence: The Object From Class That Implement Interface "IEnumerable"
    //        1. Local {Static , XML Data } :  L2Object , L2XML
    //        2. Remote : L2EF


    internal class Program
    {
        static void Main(string[] args)
        {
            #region Linq syntax [Fluent , Query]

            //List<int> Numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            #region 1. Fluent Syntax
            ////  1.1 Call "LINQ Operators" as Static Method
            //List<int> OddNumbers = Enumerable.Where(Numbers, X => X % 2 == 1).ToList();
            //var EvenNumbers = Enumerable.Where(Numbers, X => X % 2 == 0);
            ////  1.2 Call "LINQ Operators" As Extension Method[Recommended]
            //EvenNumbers = Numbers.Where(X => X % 2 == 0);

            //foreach (var num in EvenNumbers)
            //{
            //    Console.WriteLine(num);
            //} 
            #endregion

            #region 2. Query Syntax
            ////Must Be Begin With Keyword 'From'
            ////Must be End With Select Or Groupby
            //var EvenNumbers = from num in Numbers
            //                  where num % 2 == 0
            //                  select num;

            //foreach (var num in EvenNumbers)
            //{
            //    Console.WriteLine(num);
            //} 
            #endregion

            #endregion

            #region Execution Ways

            #region Deferred Execution [مؤجل]
            ////Latest version of Data

            //List<int> Numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //var OddNumbers = Numbers.Where(x => x % 2 == 1);
            //Numbers.AddRange(new int[] { 11, 12, 13, 14, 15 });

            //foreach (var num in OddNumbers)
            //{
            //    Console.WriteLine(num+" ");
            //}

            #endregion

            #region Immediate Execution 
            ////( Elements Operators , Casting Operators ,Aggregate Operators )

            //List<int> Numbers = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            //var OddNumbers = Numbers.Where(x => x % 2 == 1).ToList();
            //Numbers.AddRange(new int[] { 11, 12, 13, 14, 15 });

            //foreach (var num in OddNumbers)
            //{
            //    Console.Write(num+" ");
            //}

            #endregion

            #endregion

            #region LINQ Categories

            #region Filtration[Restrication] Operators - Where

            #region Example 1
            ////Get Elements Out Of Stock

            ////fluent syntax
            //var Result = Products.Where(P => P.UnitsInStock == 0);
            ////query syntax
            //Result = from P in Products
            //         where P.UnitsInStock == 0
            //         select P;
            #endregion

            #region Example 2
            ////Get Elements In Stock And In Category Of Meat/Poultry 
            //var Result = Products.Where(P => P.UnitsInStock > 0 && P.Category == "Meat/Poultry");

            //Result = from P in Products
            //         where P.UnitsInStock > 0 && P.Category == "Meat/Poultry"
            //         select P;
            #endregion

            #region Example 3 [Indexed Where]
            ////Get Elements Out Of Stock in first 10 elements
            //var Result = Products.Where((P, I) => I < 10 && P.UnitsInStock == 0); //Search in First 10 Index only (Valid Fluent syntax only)
            #endregion

            #endregion

            #region Transformation [Projection] Operators [Select , Select Many]
            #region Example 1
            ////Select Product Name
            //var Result = Products.Select(X => X.ProductName);

            //Result = from P in Products
            //         select P.ProductName;
            #endregion

            #region Example 2
            ////Select Customer Orders
            //var Result = Customers.SelectMany(C => C.Orders);

            //Result = from C in Customers
            //         from O in C.Orders
            //         select O;

            #endregion

            #region Example 3
            ////Select Product Id and Product Name
            ////var Result = Products.Select(X => new { ProductID = X.ProductID, ProductName = X.ProductName }); //anonymous object
            //var Result = Products.Select(X => new { X.ProductID, X.ProductName });

            //Result = from P in Products
            //         select new { P.ProductID, P.ProductName };

            #endregion

            #region Example 4
            ////Select Product In Stock And Apply Discount 10 % On Its Price
            //var Result = Products.Where(P => P.UnitsInStock > 0).Select(P => new { Id = P.ProductID, Name = P.ProductName, oldPrice = P.UnitPrice, newPrice = P.UnitPrice - (P.UnitPrice * 0.1M) });

            //Result = from P in Products
            //         where P.UnitsInStock > 0
            //         select new { Id = P.ProductID, Name = P.ProductName, oldPrice = P.UnitPrice, newPrice = P.UnitPrice - (P.UnitPrice * 0.1M) };

            #endregion

            #region Example 5 [Indexed Select]
            ////Select Product In Stock using Indexed Select
            //var Result = Products.Where(P => P.UnitsInStock > 0)
            //    .Select((P, I) => new { Index = I, Name = P.ProductName });

            #endregion

            #endregion

            #region Ording Operators [Ascending , Descending , Reverse , ThenBy , ThenByDescending]

            #region Example 1
            ////Get Products Ordered By Price Asc
            //var Result = Products.OrderBy(P => P.UnitPrice);

            //Result = from P in Products
            //         orderby P.UnitPrice
            //         select P;

            #endregion

            #region Example 2
            ////Get Products Ordered By Price Desc
            //var Result = Products.OrderByDescending(P => P.UnitPrice);

            //Result = from P in Products
            //         orderby P.UnitPrice descending
            //         select P;
            #endregion

            #region Example 3
            ////Get Products Ordered By Price Asc and Number Of Items In Stock 
            //var Result = Products.OrderBy(P => P.UnitPrice).ThenBy(P => P.UnitsInStock);

            //Result = from P in Products
            //         orderby P.UnitPrice, P.UnitsInStock
            //         select P;

            #endregion

            #region Example 4
            ////Reverse
            //var Result = Products.Where(P => P.UnitsInStock == 0).Reverse();
            #endregion

            #endregion

            #region Elements Operator - Immediate Execution [Valid Only With Fluent Syntax]

            #region [First , Last] & [FirstOrDefault , LastOrDefault] 
            ////First & Last
            ////May throw Exception if the sequence is empty
            //var Result = Products.First();
            //Console.WriteLine(Result);
            //Result = Products.Last();
            //Console.WriteLine(Result);

            ////FirstOrDefault & LastOrDefault (if the sequence is empty it will return null [Default] )
            //var Result2 = Products.FirstOrDefault();
            //Console.WriteLine(Result?.ProductName ?? "Not Found");
            //Result2 = Products.LastOrDefault();
            //Console.WriteLine(Result?.ProductName ?? "Not Found");

            ////Will throw Exceptionn if there is no matching element
            //var Result = Products.First(P => P.UnitsInStock == 0);
            //Console.WriteLine(Result);
            //Result = Products.Last(P => P.UnitsInStock == 0);
            //Console.WriteLine(Result);

            ////if there is no matching element it will return null
            //var Result2 = Products.FirstOrDefault(P => P.UnitsInStock == 0);
            //Console.WriteLine(Result?.ProductName ?? "No out of stock products");
            //Result2 = Products.LastOrDefault(P => P.UnitsInStock == 0);
            //Console.WriteLine(Result?.ProductName ?? "No out of stock products");
            #endregion

            #region ElementAt , ElementAtOrDefault

            //var Result = Products.ElementAt(10); //throw Exception if the index is out of range
            //Console.WriteLine(Result.ProductName);
            //var Result2 = Products.ElementAtOrDefault(77);//if the index is out of range it will return null
            //Console.WriteLine(Result2?.ProductName ?? "Not Found");

            #endregion

            #region Single , SingleOrDefault
            ////Single
            ////Returns: the only element in the sequence that matches the condition.
            ////Throws if: There are no elements or There is more than one matching element. (It’s a logic error if none or more than one exists)
            //var Result = Products.Single(P => P.ProductID == 5);
            //Console.WriteLine(Result);

            ////SingleOrDefault
            ////Returns:the only element in the sequence that matches the condition, null(or default) if none is found.
            ////Throws if:There is more than one matching element.
            //var Result2 = Products.SingleOrDefault(p => p.ProductID == 999);
            //Console.WriteLine(Result2?.ProductName ?? "Not Found");
            #endregion

            #region Way to use Elements Operator With Query Syntax
            ////Hybrid Syntax => Fluent Syntax + Query Expression => (Query Syntax).Fluent Syntax
            //var Result = (from P in Products
            //             where P.UnitsInStock == 0
            //             select new { P.ProductID, P.ProductName, P.UnitsInStock }).First();
            //Console.WriteLine(Result);
            #endregion

            #endregion

            #region Aggregate Operators  - Immediate Execution
            #region Count
            ////Count() => Counts elements , LongCount() => Like Count() but for large collections
            //var Result = Products.Count();
            //Console.WriteLine(Result);
            //Result = Products.Count(P => P.UnitsInStock > 0);
            //Console.WriteLine(Result); 
            #endregion

            #region Sum
            ////Sum () => Sums numeric values
            //decimal sum = Products.Sum(P => P.UnitPrice);
            //Console.WriteLine(sum);
            #endregion

            #region Max
            ////Max () => Returns the largest element
            //var Result = Products.Max();
            //Console.WriteLine(Result);
            //decimal max = Products.Max(P => P.UnitsInStock);
            //Console.WriteLine(max);

            ////var Result = Products.FirstOrDefault(P=>P.UnitsInStock == max);
            ////Result = (from P in Products
            ////             where P.UnitsInStock == max
            ////             select P).FirstOrDefault();
            ////Console.WriteLine(Result);
            #endregion

            #region Min
            ////Min() => Returns the smallest element
            //var Result = Products.Min();
            //Console.WriteLine(Result);
            //decimal min = Products.Min(P => P.UnitsInStock);
            //Console.WriteLine(min);
            #endregion

            #region Average
            ////Average() => Computes the average of numeric values
            //decimal Avg = Products.Average(P => P.UnitPrice);
            //Console.WriteLine(Avg);
            #endregion

            #region Aggregate
            ////Aggregate () => Applies an accumulator function over the sequence
            ////string[] names = new string[] { "Abdullah", "Hussein", "Ismail", "Mohammed" };
            ////var Result = names.Aggregate((str1, str2) => $"{str1} {str2}");

            //var Result = Products.Select(P => P.ProductName).Aggregate((str1, str2) => $"{str1}, {str2}");
            //Console.WriteLine(Result);
            #endregion

            #endregion

            //foreach (var item in Result)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

        }
    }
}
