// Learn more about F# at http://fsharp.net
open System
open System.IO
open System.Text
open System.Text.RegularExpressions
open System.Collections.Generic

let reFindSize = new Regex(@"^\s*(\d+)\s+(\w+)\s+(\d+)", RegexOptions.Compiled)

let readSize line =
    let foundMatch = reFindSize.Match line
    let sizeStr = foundMatch.Groups.[3].Value
    let size = Convert.ToInt32(sizeStr)
    size

[<EntryPoint>]
let main args =
    let filename = Array.get args 0
    //let filename = @"c:\temp\svn_repo_size\monitoringbackend.txt"
    let sizes = new List<int>()
    File.ReadLines(filename)
    |> Seq.filter (fun line -> line.EndsWith("/") = false)
    |> Seq.iter (fun line ->
        let tempSize = readSize line
        sizes.Add(tempSize)
    )
    let sizesSeq = List.ofSeq sizes
    let entireSize = List.reduce (fun acc size -> acc + size ) <| sizesSeq
    Console.WriteLine("Files: {0}", sizes.Count)
    Console.WriteLine("Size: {0}", entireSize)
    //let readKey = Console.ReadKey()
    0