using Linq.Data;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static Linq.ListGenerator;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

            #region Casting Operators [Conversion] - Immediate Exceution
            //List<Product> ResultList = Products.Where(p => p.UnitsInStock == 0).ToList();

            //Product[] ResultArray = Products.Where(p => p.UnitsInStock == 0).ToArray();//casting to list

            //Dictionary<long, Product> ResultDictionary1 = Products.Where(p => p.UnitsInStock == 0).ToDictionary(p => p.ProductID);//casting to Dictionary
            //Dictionary<long, string> ResultDictionary2 = Products.Where(p => p.UnitsInStock == 0).ToDictionary(p => p.ProductID, p => p.ProductName);

            //HashSet<Product> ResultHashSet = Products.Where(p => p.UnitsInStock == 0).ToHashSet(); //casting to HashSet

            //ArrayList Obj = new ArrayList()
            //{
            //    "Abdullah",
            //    "Hussein",
            //    "Ismail",
            //    1,
            //    2,
            //    3,
            //    4,
            //    5
            //};
            //var Result = Obj.OfType<string>();//Return the element of the specific type ,other elements will be ignored.

            #endregion

            #region Generation Operators - Deferred Execution
            ////valid only with fluent syntax
            ////Static Methods only (Enumerable Class)

            //var result1 = Enumerable.Range(1, 50);//(0-49)
            //var result2 = Enumerable.Range(50, 50);//(50-99)

            //var result3 = Enumerable.Repeat(2, 5);//[2,2,2,2,2]
            //var result4 = Enumerable.Repeat("Abdullah", 5);

            //var result5 = Enumerable.Empty<string>().ToList(); //List<string> result5 = new List<string>();
            #endregion

            #region Set Operators - Deferred Execution
            //var seq1 = Enumerable.Range(1, 20);
            //var seq2 = Enumerable.Range(10, 21);
            //var seqWithDublication = new List<int>() { 1, 2, 2, 1, 3, 4, 5, 5, 6, 7 };

            #region Distinct
            ////Get distinct Numbers
            //var Result = seqWithDublication.Distinct();
            #endregion

            #region Union [Concat + Distinct]
            ////Combine Numbers from seq1 and seq2 without duplicates
            //var Result = seq1.Union(seq2);
            #endregion

            #region Concat[UnionAll]
            ////Combine Numbers from seq1 and seq2 with duplicates
            //var Result = seq1.Concat(seq2);
            #endregion

            #region Intersect
            ////Get Numbers that exist in both collections
            //var Result = seq1.Intersect(seq2);
            #endregion

            #region Except
            ////Get Numbers from seq1 except those in the seq2
            //var Result = seq1.Except(seq2);
            #endregion

            #endregion

            #region Quantifier Operators [retuen bool]

            #region Any
            ////Check if there are any Products out of stock
            //bool hasOutOfStock = Products.Any(P => P.UnitsInStock == 0);
            #endregion

            #region All
            ////Check if all Products have price > 0
            //bool allHavePrice = Products.All(P => P.UnitPrice > 0);
            #endregion

            #region Contains
            ////Check if a category exists
            //bool containsCategory = Products.Select(P => P.Category)
            //    .Contains("Seafood");

            #endregion

            #region SequenceEqual
            ////Check if a Two Sequence are Equal
            //var seq1 = Enumerable.Range(1, 20);
            //var seq2 = Enumerable.Range(1, 20);
            //bool IsEqual = seq1.SequenceEqual(seq2);
            #endregion

            #endregion

            #region Zipping Operator
            ////Produces a sequence with elements from the Two Or Three specific sequences.
            //string[] names = { "Abdullah", "Hussein", "Ismail", "Mohamed" };
            //int[] numbers = { 1, 2, 3, 4 };
            //char[] chars = { 'a', 'b', 'c', 'd' };

            //var Result = Enumerable.Zip(names, numbers); //names.Zip(numbers);
            //var Result2 = Enumerable.Zip(names, numbers, chars); //names.Zip(numbers,chars);

            //var Result3 = names.Zip(numbers, (name, number) => new { index = number, Name = name });
            #endregion

            #region Grouping Operators

            ////Query Syntax
            ////Get Products Grouped by Category
            //var Result = from P in Products
            //             group P by P.Category;

            ////Get Products in Stock Grouped by Category
            //var Result2 = from P in Products
            //              where P.UnitsInStock > 0
            //              group P by P.Category;

            ////Get Products in Stock Grouped by Category That Contains More Than 10 Product
            //var Result3 = from P in Products
            //              where P.UnitsInStock > 0
            //              group P by P.Category
            //              into category
            //              where category.Count() > 10
            //              select category;

            ////Get Category Name of Products in Stock That Contains More Than 10 Product and Number of Product In Each Category
            //var Result4 = from P in Products
            //              where P.UnitsInStock > 0
            //              group P by P.Category
            //              into category
            //              where category.Count() > 10
            //              select new
            //              {
            //                  CategoryName = category.Key,
            //                  Count = category.Count()
            //              };


            ////Fluent Syntax
            //Result = Products.GroupBy(P => P.Category);

            //Result2 = Products.Where(P => P.UnitsInStock > 0)
            //                  .GroupBy(P => P.Category);

            //Result3 = Products.Where(P => P.UnitsInStock > 0)
            //                  .GroupBy(P => P.Category)
            //                  .Where(C => C.Count() > 10);

            //Result4 = Products.Where(P => P.UnitsInStock > 0)
            //                  .GroupBy(P => P.Category)
            //                  .Where(C => C.Count() > 10)
            //                  .Select(X => new
            //                  {
            //                      CategoryName = X.Key,
            //                      Count = X.Count()
            //                  });



            //foreach (var Category in Result3)
            //{
            //    Console.WriteLine(Category.Key);
            //    foreach (var product in Category)
            //    {
            //        Console.WriteLine("\t" + product.ProductName);
            //    }
            //    Console.WriteLine("=========================");
            //}

            //foreach (var item in Result4)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

            #region Partitioning Operators
            ////Take => Take Number of Elements From First Only
            //var Result1 = Products.Where(P => P.UnitsInStock > 0).Take(10);

            ////Skip => Skip Number of Elements From First And Get Rest Of Elements
            //var Result2 = Products.Where(P => P.UnitsInStock > 0).Skip(10);
            //var Page = Products.Skip(10).Take(10);

            ////TakeLast => Take Number of Elements From Last Only
            //Result1 = Products.Where(P => P.UnitsInStock > 0).TakeLast(10);

            ////SkipLast => Skip Number of Elements From Last And Get Rest Of Elements
            //Result2 = Products.Where(P => P.UnitsInStock > 0).SkipLast(10);

            ////TakeWhile => Take Elements Till Element That do not Match Condition
            //int[] numbers = { 5, 8, 4, 1, 2, 3, 7, 9, 6 };
            //var Result3 = numbers.TakeWhile(num => num < 7);
            //Result3 = numbers.TakeWhile((Num, I) => Num > I); //Indexed TakeWhile

            ////SkipWhile => Skip Elements Till Element That do not Match Condition
            //var Result4 = numbers.TakeWhile(num => num % 3 != 0);
            #endregion

            #region Let and Into [Valid only with query syntax]
            ////Into => Restart Query With Introducing A new Range
            //List<string> names = new List<string>() { "Abdullah", "Hussein", "Ismail", "Mohammed", "Ahmed" };
            //var Result = from name in names
            //             select Regex.Replace(name, "[AOUIEaouie]", "")
            //             into namesWithoutVowel
            //             where namesWithoutVowel.Length > 3
            //             select namesWithoutVowel;
            ////let => Continue Query With Added A new Range
            //Result = from name in names
            //         let namesWithoutVowel =  Regex.Replace(name, "[AOUIEaouie]", "")
            //         where namesWithoutVowel.Length > 3
            //         select namesWithoutVowel;
            #endregion

            //foreach (var item in Result)
            //{
            //    Console.WriteLine(item);
            //}
            #endregion

        }
    }
}
