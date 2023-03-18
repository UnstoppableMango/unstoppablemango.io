module App.Tests.App

open Fable.Mocha

let dummyTests =
    testList "Some tests" [
        test "a thing" { Expect.equal (1 + 1) 2 "plus" }
    ]

Mocha.runTests dummyTests |> ignore
