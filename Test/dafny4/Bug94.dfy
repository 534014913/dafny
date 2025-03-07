// RUN: %dafny /compile:3 "%s" > "%t"
// RUN: %diff "%s.expect" "%t"

ghost function foo() : (int, int)
{
    (5, 10)
}

ghost function bar() : int
{
    var (x, y) := foo();
    x + y
}

lemma test()
{
    var (x, y) := foo();
}

function foo2() : (int,int)
{
    (5, 10)
}

method test2()
{
    var (x, y) := foo2();
}

method Main()
{
    var (x, y) := foo2();
    assert (x+y == 15);
    print(x+y);
}
