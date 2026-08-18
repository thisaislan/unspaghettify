<p align="center">
  <a href="https://github.com/thisaislan/unspaghettify">
    <img src="https://github.com/thisaislan/just-images/raw/main/images/unspaghettify/cover.jpg" width="800" height="500">
  </a>
</p>

# Is There More to Game Projects Than Managers, Singletons, and Spaghetti?

> Architecture, Refactoring, and Packaging - from a developer tired of project indigestion.

I could have called this *"Ways I Found to Improve My Workflow with Unity"*, or *"Crazy Ideas I Had to Make Games"*, or even *"Let's Talk About Game Development"*. But I think the current title is a little more elegant - and it brings the right feeling. Especially the spaghetti part.

But what's the idea behind this work? You're probably wondering. And the simplest answer is: games, more specifically, game development. And yes - with Unity (one more Unity article! Nooooo).

I know, nothing new under the sun. But let me at least try to show you the idea behind it all, before you make your final decision: should you stay, or should you go?

## The Work

The idea here is simple: instead of just talking about what to do and how to do it, we'll take a simple project and walk it down a path of architectural improvement, coding improvement, and why not - fluffily improvement (That's the advantage of being on GitHub, not Medium).

Every new update will bring details about what changed, why it changed, and some discussion around the topic.

One important part of this project: I'm still writing it. In other words, we'll discover the best approach and path during development. That also means things will probably change over time. But software architecture is a dynamic exploration, so the idea of changing things over time is nothing new.

> Besides, if a project never changes, it's either too good - or too boring.

By now, you're probably wondering who I am. So...

## Who I am

My name is Aislan Tavares. I'm passionate about digital games, with occasional relapses into mobile development. I've been working as a software developer for over ten years now, mostly in the mobile space - iOS and Android - but in recent years, I've shifted my focus primarily to game development. Creating new games, building features, supporting ongoing projects, releasing DLC for all kinds of games. That's where I live now.

I was born in Salvador, a small, hot, and beautiful city in the northeast of Brazil. In 2017, I graduated with a bachelor's degree in Computer Science - with high honors. The following year, I decided to launch my first game with two friends. The goal was simple: put everything I knew to the test. So I wore every hat - software developer, artist, game designer, audio engineer, tool builder, project manager. In 2019, we happily launched Jump Box on the Google Play Store.

When I'm not studying, working, or playing video games, I make crazy little projects. Small mobile apps, unconventional web experiments, little games in different engines - Godot, Unreal, Love2D. Just for fun and to keep my knowledge sharp. If you want to see some of my crazy projects, you can find some of them on my GitHub.

> That is funny, because you are in my Github.

## What to expect

Well, as I said, I'm still writing this - so I'm not sure about every detail of the project yet. But because I have a huge amount of experience working with myself, I have a pretty good idea of where it's headed.

> I'd venture to say I have more experience working with myself than anyone else in the world. Is there a Guinness World Record for that? I should check.

The first decision: I'll use Unity. The main reason is simple - I have professional and personal experience with the engine. Beyond that, the community around Unity is enormous. And part of the idea behind this project isn't about teaching per se, but rather starting a conversation. A conversation about game development in terms of where to put what, and some of the whys along the way.

> Another part of this is the hope of being noticed. Maybe hired by Extremely OK Games or Supergiant? Haha, just kidding... or am I...?

Using Unity for this project doesn't mean the discussion is limited to that engine. Of course, libraries, components, and some implementation details will be Unity-specific. But a big chunk of the concepts, libraries, and ideas are useful in other engines too. For instance, some of the libraries I've written are already planned to be ported to Godot.

Although we'll work with Unity, I'm sorry to inform you that you probably won't see Addressables, Input System, UI Toolkit, or Localization. Well - the last one we might have a simple const system, but I can't call that a localization system. The point is to bring the discussion around project improvements. The Unity ecosystem is beside the point. Sorry if that's what you came here for.

At some level, I can say that I'm trying to fill a middle ground with this work. Ever since my undergraduate days, I've felt we don't talk enough about architecture and the health of game development. About how to keep old developers sane - and pave a path for newcomers. It's easy to find tutorials on high-level concepts, even more so about how to use specific features. But the middle ground? That's still something we should occupy a little more.

And maybe the last important detail - and you may have already noticed, given the title resemblance to another famous one and the cover image inspiration - I'll be drawing a lot of inspiration from the work of [Bob Nystrom](https://github.com/munificent). More specifically, from his book [Game Programming Patterns](https://gameprogrammingpatterns.com/). I've been using that book as my Bible for years now. Not because I'm crazy about Patterns, but because Mr. Nystrom offers us more than just a way to organize code and its relationships. He offers a clean, simple, experience-rich way to think about development. And in this field, that's priceless.

> The title was inspired by Bob Nystrom's talk, [There More to Game Architecture than ECS?](https://www.youtube.com/watch?v=JxI3Eu5DPwE), just amazing.

## Elephant in the room

AI... Yep, that's the elephant in the room. And the truth is, I'm not planning to use AI for this project. I know, I know - AI is the new big word of the moment. If I don't put a single MCP in this project, it might become an old-school kind of work. But the idea came before the most recent AI boom, and I want to keep this an experimental, human-based endeavor.

> Of course, I can't say the idea came before AI as a field - otherwise, my good friends from the [Dartmouth Workshop, 1956](https://home.dartmouth.edu/about/artificial-intelligence-ai-coined-dartmouth), might get a little upset.

Another aspect of this decision is simple: the core idea is to transfer my point of view, share my expertise, and also raise my vulnerabilities as a developer. To start a discussion about the health of game development processes. Bringing AI into the mix could disrupt the very purpose of the project.

Also, there are libraries and ideas I want to expose and test myself. I want to feel how they behave during the project's evolution and, in the end, bring my experience to the table. Again, I need to put my own hands on the project and feel things for myself.

Of course, I'm not saying I don't use AI. If you look at some of my recent GitHub updates, you'll see extensive AI usage to speed up delivery - which allowed me to reach this project earlier than expected. In fact, most (if not all) of the libraries I was working on came before the big AI moment. Each helped me speed up the process.

> But I still have that feeling: I left some bugs there because AI is too fast, and I'm just a human trying to keep tabs on a lot of projects. That feeling is distressing.

And maybe the only AI usage you'll be able to follow in this project is the text revision. Surprise, surprise - English is not my first language. So I'm using AI to revise my texts and give me tips on how to better convey my ideas.

> If you're curious about what AI tool I used for this project: [DeepSeek](https://deepseek.com/en/index.html), the free online version. Believe me, you can get a huge amount of help just with free tools these days.

## Tag system

Semantic Versioning is a widely adopted system for versioning software. Because of that, I don't think we need to discuss it at length here - but here's a quick refresher on how it works.

Given a version number **`MAJOR`.`MINOR`.`PATCH`**, increment:

- **MAJOR** version when you make incompatible API changes

- **MINOR** version when you add functionality in a backward-compatible manner

- **PATCH** version when you make backward-compatible bug fixes

> Semantic Versioning - or [SemVer](https://semver.org/) for short.

With that in mind - and knowing we can create tags on GitHub (well, actually in Git itself) - the following tag system will be established:

Given a version number **`X`.`Y`.`Z`**:

- **X** – Improvement: The efficiency and structural integrity. How much better is the project compared to the base?

- **Y** – Complexity/Progress: The sheer scale of complexity. How much more stuff exists compared to the base?

- **Z** – Vanity/Fluff: The aesthetic and personal touches. How much frivolous polish (frufruzisse!) have I added for my own delight?

By incrementing these numbers independently - and logging why we moved each one - we turn version control into a psychological mirror. We'll visually see if we're obsessed with making things work (`X`), making things massive (`Y`), or making things beautiful (`Z`).

So with that in mind, if you see a tag like `1.3.0` followed by `1.4.1` in sequence, that means I probably added some fluff - just because I wanted to improve the Unity UI probably, or something along those lines. A little more complexity, a little more fluff, but no real feature improvement to speak of. Just beauty.

> To be fair, a lot of libraries are like this... I'm not complaining. I like a little beauty in my days.

The first tag will be [v0.0.0](https://github.com/thisaislan/unspaghettify/blob/v0.0.0/README.md) - the bare-bones project, the starting point. You can find the full list of available tags [here](https://github.com/thisaislan/unspaghettify/tags).

To be fair, I don't know what the last tag will be. But I know that at some point, some of my experiments will be too much. Too niche. Too specific. So with that in mind, I'll prioritize changes that really help most people in real projects.

At some point, I'll reach a place where I can say: "This is a good point". A place where I've touched on all the crucial points we need to cover. When that happens, I'll come back here and share that tag.

That way, if you ever want to see where this project thinks is a nice way to work, you can find it easily.

## See Also

We've covered a lot, but some tools and concepts were just mentioned in passing. If you want to dig into those concepts and tools a little more, here's the full list.

- [**MCP**](https://modelcontextprotocol.io/docs/2026-07-28/getting-started/intro) - Model Context Protocol, is an open-source standard for connecting AI applications to external systems.

- [**Git**](https://git-scm.com/) - A free and open source distributed version control system designed to handle everything from small to very large projects with speed and efficiency.

- [**Github**](https://github.com/) - Proprietary platform for developers that allows them to create, store, manage, and share code. It uses Git to provide distributed version control, and GitHub itself offers access control, bug tracking, software feature requests, task management, continuous integration, and wikis for each project.

- [**Tag**](https://git-scm.com/book/en/v2/Git-Basics-Tagging) - Like most VCSs, Git has the ability to tag specific points in a repository’s history as being important. Typically, people use this functionality to mark release points (v1.0, v2.0 and so on).

- [**Addressables**](https://docs.unity3d.com/2021.2/Documentation/Manual/com.unity.addressables.html) - The Addressables system provides tools and scripts to organize and package content for your application and an API to load and release assets at runtime.

- [**Input System**](https://docs.unity3d.com/Manual/Input.html) - The Unity Input System is a flexible package that allows developers to manage input from various devices, replacing the older Input Manager. It supports event-driven interactions and can be installed via the Unity Package Manager for projects using Unity 2019 LTS or later

- [**UI Tollkit**](https://docs.unity3d.com/Manual/UIElements.html) - UI Toolkit is a collection of features, resources, and tools for developing user interface (UI).

- [**Localization**](https://docs.unity3d.com/2021.3/Documentation/Manual/com.unity.localization.html) - Use the Localization package to easily configure localization settings for your application.

## Furthermore

This work is trying to raise discussion - so bring it to friends, colleagues, peers, to everyone. A lot of the points here will go against the most common techniques and patterns in the market. And that's okay, we can't make an omelet without breaking some eggs.

With more options, more ideas, and more fields involved, we increase the chance of creating more accessible and democratic projects. Projects where everyone involved is, well... really involved.

And last but not least: be gentle.

***Be gentle with yourself. Be gentle with others. Be gentle with everything involved.***

Building anything is a complex and hard process, full of opposing ideas and perspectives. But no one said it should be disrespectful - or even unfun. After all, we're creating games. And what would games be without fun?

: )

<p align="center">
  <a href="https://github.com/thisaislan/unspaghettify/blob/v0.0.0/README.md">
    next</a>
</p>

