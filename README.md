<p align="center">
  <img src="https://github.com/thisaislan/just-images/raw/main/images/unspaghettify/v0.1.0.png" width="800" height="500">
</p>

# A Pinch of Taste in the Code

Okay, I must confess: I am not the biggest fan of C#. Although I have worked with Unity for quite a while now, I still feel that the language is powerful but a little clumsy, verbose, or just weird. But of course, none of that can stop a developer from putting their own taste into the code and adding some touches of class to it (okay, class is debatable).

> If you want to know the kind of language I like, I can say [Kotlin](https://kotlinlang.org/),  [Swift](https://www.swift.org/), or [Lua](https://www.lua.org/).

The idea here is to start adding a little of myself to the code. Until now, only the AI has worked on it. Of course, I won't change a huge amount of code now, just small details to prepare for the next steps. Because of that, the tag didn't increase that much.

An important point is that I also tried to clean up the code a little, so some classes now look less complex, with more reusable code and better use of what was already there. Of course, we still have a huge chunk of work ahead, and I will bring more ideas that can be used in a project to improve its health. But by the end of this section, you will get a good idea of how the project works and the directions it will probably take in the next steps.

Without further ado, let's dive a little into the code.

## Small Changes

Here we cover the small details you will probably see when you check the code, but they are not a big breakthrough. You know those kind of changes we do because, like any developer, we have our own way of writing code and, of course, our own idea of beauty. Of course I have a lot of flexibility here, I am working alone in that project, but it is important to say that when work in a team, we always work as a group. So the best approach is something the whole group is comfortable with, even if that goes against common marketing practices.

> A bad pattern is better than no pattern at all

The first thing I want to talk about is braces. The language lets you skip them for a single statement, but code gets edited far more often than it gets written, and the moment someone adds a second line to a braceless `if`, the indentation can lie to you. It looks guarded, the compiler disagrees, and that is exactly the kind of bug that wastes an afternoon.

There is also the classic dangling `else` problem, where the `else` binds to the nearest unmatched `if` no matter how the indentation makes it look. The [SEI CERT](https://cmu-sei.github.io/secure-coding-standards/sei-cert-c-coding-standard/recommendations/expressions-exp/exp19-c/) (secure coding standard from the Software Engineering Institute at Carnegie Mellon University) guideline has a security example where that ambiguity hands out admin privileges, and Apple's `goto fail`  bug is the famous real-world version of the same trap. Braces remove the ambiguity completely.

So yes, C# allows braceless bodies, and yes, some people find them lighter to read. But the cost of braces is two lines and a little visual noise, and the cost of skipping them is a whole category of silent bugs. That is why I put braces on every `if`, `else`, `for`, `foreach`, `while`, and `do`, every time.

  > A bad pattern is better than no pattern at all, but a pattern that prevents silent bugs is better than both.

Another point you will problably notice is the way I change some comments. I beleave comment above the declaration beats a trailing comment almost every time. It is easier to read, easier to wrap when it gets long, and it does not get pushed off-screen by a long declaration or require alignment. Trailing comments are fine only for very short labels, but if the explanation actually matters, put it above.

In Unity, remember that plain comments do not show in the Inspector. That is why `[Tooltip]` is usually the better choice for serialized fields, since the designer can hover the field and see what it means. Use `/// <summary>` when you want IDE documentation in Visual Studio or Rider, and that is most useful for public APIs, shared code, or package code.

And avoid redundant comments like `// The base points` above `public int basePoints;`. If the comment cannot tell the reader something the code does not already say, it is just noise with a `//` in front of it. A comment is a message to the next person, so if the message is, I ran out of ideas for names, perhaps the best way to use your time is to try to improve the name.

One thing you problably noticed too is I am not a big fan of `var` in C#. The problem with `var` is not that it is always wrong. It is that it hides the type at the exact point where the reader is trying to understand the code. When you write `int count = 0` or `float speed = 1.5f`, the type is part of the sentence. When you write `var count = 0`, the compiler still knows the type, but the next developer has to infer it from the right side of the assignment. In a short, obvious line, that is fine. In a longer method, or in a line where the right side is a method call with a vague name, it can turn a simple declaration into a small puzzle. That is the kind of confusion I want to avoid.

Maybe the last small noticeable change is the way I organize the code. Here again I have to say that C# is a verbose language, and adding the Unity API on top can make it a little too complicated to create organized classes and keep the navigation easy. You can find solid best practices for C# member ordering in the [Microsoft C# coding conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions), the [Google C# Style Guide](https://google.github.io/styleguide/csharpguide.html) , and [SonarSource's C# coding style guidelines](https://github.com/SonarSource/sonar-dotnet/blob/master/docs/coding-style.md). Unity's own manual on [naming scripts](https://docs.unity3d.com/Manual/naming-scripts.html) covers file naming and namespace organization. There are also community guides that go deeper into Unity project structure and assembly definitions.

> A quick note on the SonarSource link: their full rule catalog now lives inside SonarQube and asks for a login, so I pointed to the public `coding-style.md` from their `sonar-dotnet` repo instead.

But it is complicated to guarantee all C# situations, even more so to keep all of that in mind while you are actually building something. So in my project I follow a loose organization, one that keeps the code more readable and simple to manage, rather than forcing every file into a rigid template. The goal is not to win a style contest. The goal is to open a script six months from now and still know where things are.

## Not so Small Changes

The idea behind this section was to explore, as the title suggests, all the not-so-small changes. These are changes that I consider more than just a whim of mine, but details to which we really should pay a little more attention when creating a project. At a certain point, I began to realize that only two things stood out enough to be included here.

Without wasting more time, let's talk about the first one: the `ClickableBehaviour` class. Although I am not sure about that nomenclature (SomethingBehaviour), the idea of easily grasping what the class/file does/contains is always welcome. That is why we have the `I` in front of interfaces. But I digress. The point about that class is reusability: it takes a behaviour that the code was repeating in different places and gives it one home.

Looking at the code, the class does three things. It holds the `isAlive` flag and the countdown coroutine, it defines the abstract `ClickPont()` and `StartCountDown()` for the concrete classes to fill in, and it wraps the `OnMouseDown` callback so that clicks only reach `OnMouseDownAction` while the object is alive. The `Destroy()` method is also guarded, so calling it twice does nothing. None of this is clever. It is just the boring, repeated part of a thing you can click that can die pulled out of the `Bomb` and `Fruit` classes so those classes can focus on what makes them different.

Of course, in some cases you don't have time to reuse everything, or the cost in time or complexity is too high. In these cases it is better to keep nice and simple code that is readable than an incredibly reusable code. And of course, maybe a comment will help the next developer understand the whole situation.

Keep reusability in mind, and you will probably notice the removal of the `Update` method in the `Bomb` and `Fruit` classes. Instead, we now use `IEnumerator`. The advantage is that the countdown does not need to check a timer every single frame. It yields, waits, and resumes when the time is right. For a countdown that ticks once per second, that is a much better fit than an `Update` that runs sixty times per second just to subtract from a float. As a rule of thumb: if it runs every frame, be intentional with `Update`. If it is time-based, a coroutine is usually cleaner. [Unity's own manual](https://docs.unity.com/en-us/engine/6000.6/manual/scripting/coroutines-section/coroutines) also points out that a coroutine that runs almost every frame without pausing is usually better replaced by `Update` or `LateUpdate`, which is exactly the trade-off we are making in the other direction.

> Of course, there are more. small and big changes we could put in that and in the previous section. Literal string elimination, better names for references and methods, organization of the code itself and so on. But I don't think we can cover every small detail in these sections, otherwise they will become huge and a little tedious. The idea here is to cover the most interesting points with enough context, so we can build something together. Also, feel free to dive into the code and check the changes for yourself, but keep in mind, there are more changes to come. That was just the first trip, only to test the waters.


## Helping Your Code

Probably no text that intends to be about code will escape what we call here a soup of letters. This one is no different. I can't guarantee I'll apply every definition in the code, but since the idea of that project is to explore, let's explore a little some of the famous acronyms we use in code development.

### SOLID

The famous SOLID model was originally presented as five principles by Robert C. “Uncle Bob” Martin in his 2000 article. [Design Principles and Design Patterns](https://web.archive.org/web/20150906155800/http://www.objectmentor.com/resources/articles/Principles_and_Patterns.pdf), and Michael Feathers later arranged in an email the initials into the now-familiar acronym. The motivation was software rot: Martin acknowledged that successful software will change, and the principles are a bet that certain shapes of code handle that change with less pain.

**S - Single Responsibility:** A class should have one reason to change. Not in the vague sense of doing one thing, but one reason: one group of people who might ask you to change it. If two different people could come to you with two different requests, that is two responsibilities, and the class is carrying too much. Picture a class that handles both invoices and emails. The finance team wants one change, the marketing team wants another, and now every edit risks breaking the other side.

**O - Open/Closed:** Open for extension, closed for modification. You add new behavior by adding new code, not by reopening old code that already works and editing it. Think of a power strip: you plug in a new device without rewiring the house. The point is to protect the parts that already work from the ripple of a new requirement.

**L - Liskov Substitution:** Barbara Liskov's idea, in plain terms: if your code expects a bird, then a penguin should work there too, without the caller noticing anything strange. If your subclass throws new errors or quietly changes the rules, it is not really a subtype. It is a trap with a familiar name on the door.

**I - Interface Segregation:** Don't force people to depend on methods they never use. A fat interface is like a job contract that makes you sign for every task in the company, including the ones you will never touch. So you end up writing empty methods or throwing not supported just to satisfy the paperwork. Smaller, role-specific interfaces keep that pressure off.

**D - Dependency Inversion:** Depend on abstractions, not on concrete details. The high-level rule (send a notification) should not be glued to the low-level detail (send it through this specific email service). Both should meet at an interface in the middle. The inversion is that the usual direction flips: the detail now depends on the abstraction, and the abstraction becomes the stable point.

> The honest catch: these five do not always agree. Treating them as a ranked law makes the conflicts harder, not easier. Martin's own framing is about handling change, not about scoring purity. And for someone with little practice, that is the useful part: you are not failing a test, you are making a trade-off.

### CUPID

Daniel Terhorst-North wrote CUPID in 2022 as an alternative to SOLID, in his article [CUPID: for joyful coding](https://dannorth.net/blog/cupid-for-joyful-coding/). His point there was simple: SOLID gives you principles to argue about, while CUPID gives you properties you can look at and ask, “Does this code have them?” The goal is not to pass a purity test. The goal is code that is a joy to work with.

**C - Composable:** It plays well with others. You can combine it with other pieces without hidden assumptions about order, context, or side effects. Think of Lego: the pieces connect because they agree on the shape of the connection, not because they know what the whole castle looks like. Small pieces can become a system without each piece knowing the whole.

**U - Unix philosophy:** Do one thing well. A function, class, or service should have a narrow job and a clear contract, and it should resist the urge to absorb every nearby concern. It is the difference between a knife and a Swiss Army knife. Both are useful, but only one is easy to reason about when something goes wrong.

**P - Predictable:** It does what you expect. Same inputs and same state, same behavior. No hidden mutation, no results that depend on timing, no undocumented exits. Predictability is what makes code possible to reason about. If you cannot predict it, you cannot trust it.

**I - Idiomatic:** It feels natural. The code follows the conventions of its language and ecosystem, so a reader does not have to translate a personal dialect. Idiomatic code is not about style points. It is about lowering the cost for the next person who has to read it.

**D - Domain-based:** The code models the problem domain in its language and structure. Names, boundaries, and flows reflect the actual business or problem being solved, not some internal technical convenience. This is what keeps code habitable as the domain grows and changes.

> The honest catch: CUPID came partly from a playful pushback against SOLID, but the question underneath is serious. What properties make code joyful to work with, rather than merely correct by a checklist? For someone with little practice, that is the useful shift: you are not memorizing laws. You are looking at the code and asking whether it has these qualities.

### DRY: Don't Repeat Yourself

Every piece of knowledge in a system should live in one place. The point isn't never type the same characters twice. The point is that when a rule changes, you change it in exactly one spot. The opposite is WET: `Write Everything Twice`, or `We Enjoy Typing`. The joke makes the cost obvious: every copy is one more place a bug can hide, and one more edit you owe future-you. And future-you, let's be honest, will not remember why there were two copies.

### KISS: Keep It Simple, Stupid

The simplest design that solves the problem is the right one. The Norwegian architect Wenche Selmer spent her career perfecting the cabin: frugal, useful, adapted to its surroundings, with space reduced to the bare minimum. She embraced what she called a rich life based on simple means, and she asked her clients and students the same question: What can you do without? Code carries the same risk she was guarding against. A design too clever to strip down is a design that fails when it matters. The next person to read your function might be you in six months, with no memory of why you wrote it.

> And yes, the S in the acronym is a little rude. Some people soften it to `Keep It Simple, Sunshine`. My favorite is `Keep It Simple, Silly`” because it works whether you are talking to a colleague or quietly to yourself after the third bug of the day.

### YAGNI: You Ain't Gonna Need It

Building a generic, configurable helper for a duplication you predict for later is guessing. Write the second copy when the second case is actually real, then extract. The rule of three says wait for the third time before you abstract. It's a brake on your own enthusiasm, and honestly, that is the whole point.

### BDUF: Big Design Up Front

The old waterfall dream: finish and perfect the design before writing a single line of code. It sounds responsible. In practice, the design often goes stale the moment coding starts, and the team has spent months polishing a map of territory that already moved.

> And the catch is real, but it is not that planning is bad. It is that BDUF treats the plan as a contract when it should be a hypothesis. Agile grew up as the reaction to this: short cycles, working software, feedback from real use. Games are a nice example because they live in both worlds. A studio with a publisher and a milestone contract cannot simply wander into the fog, so it plans. But the best designers treat the game design document as a compass, not a script, because fun is something you find by playing, not by declaring it on page forty. BDUF stops being a problem when the territory is actually stable: established tech, a sequel, a port, a safety-critical system where a late change costs more than the planning ever would. It becomes a problem when the map is assumed to be the ground. Plan enough to avoid the obvious dead ends, then walk.

### DIE: Duplication Is Evil

It restates DRY with a sharper edge, focused only on the cost of copied code. Every duplicate multiplies the work of a bug fix, since you patch the same defect in every copy, and it invites contradictions when the copies drift apart. The name is dramatic. The tax is real.

### GIGO: Garbage In, Garbage Out

A program's output is only as good as its input. Feed it inaccurate data and you get inaccurate results, no matter how correct the code is. It is the quiet argument for input validation, data cleaning, and sanity checks. The code can be beautiful. If the data is wrong, the result is wrong.

> Games make this painfully concrete. A damage formula can be a masterpiece of clean design, but if a designer types the wrong multiplier into a balance spreadsheet, the boss one-shots the player and the bug report says combat is broken. Same story with a save file corrupted by a bad migration, or a level table with one enemy spawning at the wrong coordinates. Nobody blames the data. They blame the code, because the code is what they can see. So validate at the edges where data enters, from config files, spreadsheets, the network, and player input, and fail loudly when something is off. A sanity check that catches a bad number on day one saves you a week of chasing a phantom bug that turned out to be a typo in a CSV.

> And here's the part I keep relearning: these principles pull against each other. DRY versus KISS. DRY versus YAGNI. Open/Closed versus YAGNI. Knowing which one yields is the mark of an experienced developer, not knowing all the definitions by heart. The resolution is almost always the same: solve the problem in front of you with the simplest code that works, and refactor toward a principle when a second concrete case proves the abstraction earns its cost. The danger isn't ignorance of the acronyms. The danger is obsession: treating a principle (or practically anything in the world of programming) as an unbreakable law, applying it everywhere, and turning a tool for clarity into a source of headaches. The code is supposed to serve a purpose: helping people, not the other way around.

## See Also

- While I was writing these sections, I came across this magnificent work by [Women Who Code](https://womenwhocode.com/), [Squeaky Clean Code: How to Develop & Adopt Better Coding Practices](https://womenwhocode.com/blog/squeaky-clean-code-how-to-develop-adopt-better-coding-practices/).

- If you, like me, strive to choose beautiful names for everything and are always creating a new standard for doing things, here's a, probably too big, list of name references and conversions (pelase, go easy on this): [Microsoft Common C# code conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions), [Unity Script file names and namespaces ](https://docs.unity.com/en-us/engine/6000.6/manual/scripting/get-started/naming-scripts), [Naming and code style tips for C# scripting in Unity](https://unity.com/how-to/naming-and-code-style-tips-c-scripting-unity#identifier-names), and this beautiful work by Felienne Hermans, [How patterns in variable names can make code easier to read](https://devblogs.sh/posts/felienne-hermans-how-patterns-in-variable-names-can-make-code-easier-to-read).

- Because we had already talked about `Update` vs `IEnumerator`, maybe I can hint a little the future and introduce here the [Unitask](https://github.com/darsausalo/UniTask). Have fun.

- If you want to see more about SOLID and CUPID with code examples as a plus, maybe that article from [Developers Voice](https://developersvoice.com/) can help, [From SOLID to CUPID: Design Principles That Survive Production in Cloud-Native .NET](https://developersvoice.com/blog/architecture/solid-to-cupid-playbook/).

- Also that good-humored article form [Geeks Programming](https://geeksprogramming.com) can help you with acronyms, [SOLID, DRY, KISS, YAGNI: Design Principles](https://geeksprogramming.com/key-principles-in-software-and-acronyms/).

- We briefly mentioned software rot earlier. To understand it, it helps to look at where it comes from. Take the classic 1997 interview [Elegance and Entropy](https://www.salon.com/1997/10/09/interview_9/) from Salon, with Ellen Ullman, where she describes the programming life as a "constant tug of war between the computer's demand for exactitude and the entropic chaos of real life."


</br>
<p align="center">
<a href="https://github.com/thisaislan/unspaghettify/blob/v0.0.0/README.md">
    prev</a>
    |
  <a href="https://github.com/thisaislan/unspaghettify/blob/index/README.md">
    index</a>
    |
  <a href="https://github.com/thisaislan/unspaghettify/blob/soon/README.md">
    next</a>
</p>
