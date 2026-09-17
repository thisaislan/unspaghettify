<p align="center">
  <img src="https://github.com/thisaislan/just-images/raw/main/images/unspaghettify/v0.0.0.png" width="800" height="500">
</p>

# Base Project

Okay, I lied. I used AI. I said I would avoid using it to create the project, but at some point I thought it would be interesting, because in some sense, it would be a nice way to delegate the creation of the project to another being, thus avoiding prematurely adding my ideas and point of view. Other than that, asking an AI to create the project (probably) ensures that the way the project was created reflects some of the code used nowadays without too much complexity or overengineering.

> I must confess I didn't give much thought to the prompt. The idea was to get a rough draft, so I just asked for the basic structure, without too many details.

But I'm standing here describing the project rather than showing it, which is a terrible approach for games. Simply talking about something instead of demonstrating it rarely serves the narrative well. Before I go to the next section and explain the humble project itself, [here](https://thisaislan.github.io/fruit-click/) is a link to view the project online. All new versions will be available on that page, allowing us to follow the project's evolution step-by-step.

## Fruit Click

Fruit Click is a small reflex game. You get 60 seconds to click fruits that pop up randomly while dodging the bombs mixed in. Miss a fruit? No penalty, just a lost chance. Click a bomb? Minus 10 points. Ouch.

Spawns start around every 2 seconds and speed up to about every 0.5 seconds, with a max of 6 items on screen - intense, but still playable. Each fruit (or bomb) appears fully inside the screen, thanks to spawn logic that accounts for item size. A weighted table decides what shows up: cheap fruits are common, rare high-value ones are, well, rare. About one in five spawns is a bomb.

Every fruit has a progress bar that drains over its lifetime. Click it before the bar empties to earn its base points - higher-value fruits have shorter timers, so they're harder to catch.

The UI is simple: score top-center, countdown bottom-center, pause button top-right. When the timer hits zero, the round ends and the pause button turns into a replay button, press it to play again. Very dramatic.

In short: tap fruits for points, dodge bombs, and survive the rising spawn rate before the clock runs out.

> A masterpiece. You might find one or two like that on the market, but this one still feels totally fresh. Joking aside, I avoid copy-pasting the [Fruit Ninja](https://www.halfbrick.com/games/fruit-ninja-classic) idea to simplify the project.

## What I did?

As I pointed out before, I started asking the AI to create the game, but since I was using the web version of DeepSeek, I needed to ask via a prompt to create the scripts and the step-by-step of how to implement the project. Although I think I still remember how to implement a simple project in Unity (I think), the idea here is to use the AI as an external thinking mind, so I avoid adding my own point of view at the start, but I still gain some knowledge about that project.

Using [Unity version 6000.6.0f1](https://unity.com/releases/editor/whats-new/6000.6.0f1), I started with an empty Universal 2D project, deleted the welcome package, and used [gitignore.io](https://www.toptal.com/developers/gitignore) to create the [gitignore](https://git-scm.com/docs/gitignore) file. As you can see, there's nothing fancy or any shenanigans in the settings.

> If you want to create the same gitignore file, just click on that [link](https://www.toptal.com/developers/gitignore/api/unity,visualstudiocode,rider). It is worth noting that I am not advertising here; I simply think this type of tool should be used more in daily life to help people with their projects.

After that, I just started follow the AI steps, and tah dah, we have Fruit Click. And, exactly as I predicted, I couldn't resist adding a few little things, as you can see. I added some sprites and a font too and below you can see all the assets I used, alongside the link where I found each one:

  - **Fruits icon pack**, Robert Brooks - https://gamedeveloperstudio.itch.io/fruits-icon-pack
  - **Cozy UI Pack**, dobo_ui - https://dobo-ui.itch.io/cozy-ui
  - **Bomb Asset**, Thelma Carr - https://freepngimg.com/png/100213-bomb
  - **Sillyfox Font**, KURO GAMES ENTERTAINMENT - https://kuroo-games.itch.io/sillyfox-font-3


Well, maybe you're thinking: why did I put sprites and even a font in the project already? The answer is simple: since the core is already done - although it's not a complicated one - adding some juice as soon as possible is a nice way to start feeling the project, test the art concept, and even keep the developer (in this case, me) a little happy. Although maybe simply adding images and fonts can't be called "juice," I'd say it helps me avoid adding more "ugly" things during development, since I'm seeing "nice" things - something like the [Broken Window Effect](https://www.joelvanderweele.eu/wp-content/uploads/2021/11/BrokenWindows.pdf). But that's not my field, and I think we could talk about it in a game design section.

> Yeah, maybe you noticed I ignored the audio settings here in the juice process. Please don't do that in your game. Maybe in a real project I could think about the audio side at the same moment I think about the image side; both will help to craft the experience, and in the end, the gaming experience is the game itself.

## Game Design

OK, I added that chapter because I thought it would be boring to only present the project and explain it. But in the end, I think it works well — speaking directly about some game design improvements that could be made in this project, even though I don't think I'll implement all of them. Maybe just a few.

Without futher talking, let's start with the one we had already talked about before:

### Juice
 
The term was first coined in the article [How to Prototype a Game in Under 7 Days](https://www.gamedeveloper.com/game-platforms/how-to-prototype-a-game-in-under-7-days). In game development, it means that extra layer of feedback that makes a game feel responsive, satisfying, and alive. Adding sound effects early can definitely be part of juicing. Adding nice art assets early, though, isn't automatically "juice" - usually only if those assets are used as responsive feedback to player actions.

> We can see the concept/idea of juice being mixed with polish or an early art/audio pass, but I'm not here to debate the terminology - just to share the nice things.

It's possible - and perhaps desirable - to add juice throughout every phase of the game's development. That said, applying some juice early can help, it lets developers feel the experience sooner, allowing changes and improvements early on too. It can also help validate user experience feedback and concepts.

Of course, some balance is necessary here. Perhaps the best moment to do that is after closing the core or after the main features are in, so the task of adding juice doesn't cannibalize other important tasks. Every game has its own pace, and the team needs to figure out the best moment to do that.

> To be fair, I love the final stages, where we add juice and do the polishing - it’s like taking something that already shines and making it shine even brighter. The only problem is knowing when to stop.

### Randomness

As you could see in the project description, we count a lot on randomness: the fruits appear in random positions, and the fruit itself - or the bomb - is a random choice, even using some weight. Whether or how computers can really generate randomness, or the distinction between pseudo-random vs true random, is beside the purpose of this section. But the usage of some trick to create a nice experience using them is, happily, not.

> Before proceeding, I avoided delving into every topic (or potential topic) here, to avoid information overload. However, you will find more information in the `See Also` section, if you wish to explore further.

#### Fruit Selection

Although randomness looks like a fair way to deal with the fruit selection or even a bomb, nothing can guarantee a nice experience when the whole selection is random. Same fruits can appear a lot of times, or never appear. Also, from a game design point of view, that removes control of the situation, leaving the experience at the mercy of chance. I decided to use a weight system to try to balance things, but there are more interesting ways to allow some feeling of randomness without messing with the game design.

One of the ways I brought to the table at the start of the project is the 7-bag system, or any variant, like in the [Tetris](https://tetris.com/) game. The idea behind the 7-bag system is to keep a "bag" containing exactly one of each of the seven tetrominoes: I, O, T, S, Z, J, and L. At the start, the bag is filled with those seven pieces and then shuffled into a random order. When the game needs a new piece, it takes the next one from the front of the bag. Once the bag is empty, you create a fresh bag with one of each piece again, shuffle it, and keep drawing from it.

This will allow the person to see all the fruits and ensure that a specific fruit is never further away than the total number of fruits.

But maybe the best idea here would be to create pre-made bags with a preset of fruits. This way, the person playing can get the feeling of “knowing” the next fruit selected in the game. For example, if I have the following bags:

```
Bag 1 - A, B, D
Bag 2 - A, C, E
Bag 3 - F, E, C
```

> I chose letters here only to simplify the explanation.

By the time the person sees an `F` appearing, they know the following two pieces. Of course, we can get random elements in the bag, creating a certain sensation for the person. But still, if the element `F` appears, and no `E` or `C` has appeared yet, most people who have played the game enough times will intuitively guess the next possible pieces.

Another advantage of that approach is that we can choose which bags will appear in each stage, in case of a game with multiple stages. This allows some level of control, and also let us know the maximum (and minimum) number of points possible in each stage. For instance:

```
Stage 1 - Bag 1, Bag 2
Stage 2 - Bag 2, Bag 3
```

> `D` and `F` had a problem in the past and can't play along...

Of course, here a little randomness can help set the tone of the experience. Although the bags are previously selected, the order in which they appear can be random.

The bag system alone gives us options for the game's tone: from something that looks totally random to tightly controlled, predictable gameplay for more experienced players. The choice should be based on the experience we have in mind.

Alongside weight - the one the project is using - and bag, maybe we could also talk a little about the history-based system. The [Tetris: The Grand Master (TGM)](https://tetris.com/products/video-game/tetris-the-grand-master) series is the pioneer here. It keeps a 4-piece history. When generating a new piece, it "rolls" a random piece multiple times (4 tries in TGM1, 6 in TGM2) until it finds one that isn't in the history. If all tries fail, it settles for a recent piece. This method effectively minimizes immediate repeats, but it doesn't offer the strict mathematical guarantees of a bag system.

#### Fruit Position

In the same way as the fruit selection, the position is random in the project. That can be totally ok, but if you have already played games like [Just Dance](https://www.ubisoft.com/en-us/game/just-dance), [Guitar Hero](https://en.wikipedia.org/wiki/Guitar_Hero) or [Beat Saber](https://www.beatsaber.com/), you will notice that these games have some kind of rhythm, some kind of direction to the motion. Things do not appear in any random position. Even in Fruit Ninja, the fruit positions can bring some ordered aspect when they appear.

Well, not here. But we can bring some order to the chaos with fill tricks, and again, it depends on the kind of experience we want to create.

##### Minimum Distance Between Elements

One of the most common techniques is Poisson Disk Sampling. Instead of scattering points completely at random, it guarantees a minimum distance between each one with one simple rule: `no two things can be closer than a certain minimum distance`. Think of a forest: trees don't grow on top of each other, but they also don't form a perfect grid. That's the kind of organic yet orderly spread you get. It's perfect for placing trees, rocks, or spawn points so they never clump together in a way that feels messy.

##### Fill The Space Without Losing Memory

Another approach is using Halton or Sobol sequences. Imagine you want to scatter points across a square, but you don't want them to clump together or leave big empty gaps. A Halton sequence is a formula that gives you a list of points which look random, but are actually carefully spaced to fill the space as evenly as possible. The trick is `digit reversal`.

Let's say you're using base 2. You count normally: 1, 2, 3, 4. For each number, you:

1. Write it in binary.
2. Reverse the digits and put a decimal point in front.
3. Convert back to a decimal.

Doing it for the first few numbers:

| Count | Binary | Reversed | Decimal |
| --- | --- | --- | --- |
| 1 | 1 | 0.1 | 1/2 |
| 2 | 10 | 0.01 | 1/4 |
| 3 | 11 | 0.11 | 3/4 |
| 4 | 100 | 0.001 | 1/8 |
| 5 | 101 | 0.101 | 5/8 |

So the sequence is: 1/2, 1/4, 3/4, 1/8, 5/8.

Notice how it jumps around, but never lands too close to a previous point. It "fills in the gaps".

For 2D points, you just use two different bases:

```
X coordinate: base 2
Y coordinate: base 3
```

For 3D, add a Z coordinate: base 5. Each new dimension uses the next prime number.

A famous example is [Spore](https://www.ea.com/games/spore/spore), which used an incremental Halton sequence to distribute objects. You can even use the sequence index to vary attributes like color or scale, so similar objects end up far apart.

##### Cell based

Often used for maps and levels, Wave Function Collapse (WFC) is a procedural generation technique. It looks at a set of example patterns you give it, then creates new layouts that follow the same rules. With some changes we can use it here for spawn waves.

Imagine an empty grid where each cell will eventually hold one spawn tile. At the start, every cell can be almost any tile. WFC doesn’t fill the grid in order. Instead, it finds the cell with the fewest possible tiles left, chooses one of them, and places it there. That choice affects the neighboring cells: based on the example patterns, some tiles can no longer sit next to the chosen tile, so WFC removes those tiles from the neighbors’ options. That can cause more removals in their neighbors, and so on. WFC repeats this - pick the most restricted cell, place a tile, remove incompatible options nearby - until every cell has exactly one tile.

The result is a spawn pattern that feels locally random but globally organized. The player sees variety, but the wave has a logical flow - like the fruit is coming at them in a designed sequence, not just popping up wherever.

It's like solving a puzzle where the pieces are your fruit formations, and the rules are `what can sit next to what`.

##### Full control

For highly authored experiences, nothing beats a [data-driven](https://www.dataversity.net/data-concepts/what-is-data-driven/) spawn system. You can define waves, timing, and positions in a [CSV](https://formatarc.com/en/blog/what-is-csv/), [JSON](https://www.json.org/json-en.html), or a custom editor tool. Then a spawn system reads that data and executes the patterns. This lets designers iterate quickly without touching code. Think of rhythm games where every beat has a specific spawn point and enemy type - that's data-driven design at work.

## Or, Do It Yourself

Something important to say is - and maybe I should add that section before the game design section - you do not need to know everything about game design, or a specific style of game, or all the techniques to create games before you start your game - or even release it. Of course, some specific knowledge would help you and has already been tested, which can bring some security, but games are also an expression and a vision of ideas.

During the development of that chapter, I came across the video [How Balatro Was Made and Why The Creator Expected to Sell Only 6 Copies](https://www.youtube.com/watch?v=g86eP48WN78) on [ThatGuyGlen channel](https://www.youtube.com/@ThatGuyGlen) and found out that [LocalThunk](https://localthunk.com/), the creator of the hit game [Balatro](https://www.playbalatro.com/), never played a [deck game](https://www.gaming.net/what-is-a-deck-building-game/) before creating his own, but the game is amazing - seriously, play it if you never have.

Well, I think the last thing I should say here is: be yourself, and of course be prepared for some people not to fall in love with your project. That is ok. No game is perfect for every person in the world; we have distinct lives, points of view, and tastes - and that is beautiful and should be respected. At the same time, we should respect our own point of view, our team, and our project as a whole. So I know that is not easy - just try to play fair and be understanding; creating a game is a long but unique process.

> In some cases, even you will think your game has problems - In fact, often - but that is ok. I encourage you to watch the video [Balatro's 'Cursed' Design Problem](https://www.youtube.com/watch?v=zk3S3o1qOHo) from [Game Maker's Toolkit](https://www.youtube.com/@GMTK) and see a little more about the "bigger design flow" through the designer's own vision.

## See Also

- [Juice it or Lose It](https://gdcvault.com/play/1016487/Juice-It-or-Lose) by Martin Jonasson & Petri Purho, is widely considered the definitive talk on game juice.

- [The art of screenshake](https://www.youtube.com/watch?v=AJdEqssNZ-U) by Jan Willem Nijman is another classic that maybe you should check.

- If you are interested in discussions about truly random numbers, you might like the article [Can a computer generate a truly random number?](https://engineering.mit.edu/ask-an-engineer/can-a-computer-generate-a-truly-random-number).

- But, if your interest is specifically the aspect of randomness in games, these contents are for you: [Take a Chance: The Illusion of Randomness in Games](https://www.gamedeveloper.com/design/take-a-chance-the-illusion-of-randomness-in-games), [How and why game devs manipulate 'luck' in games like Peggle](https://www.gamedeveloper.com/design/how-and-why-game-devs-manipulate-luck-in-games-like-i-peggle-i-) and [The History of Randomness (and how gamblers invented probability)](https://www.youtube.com/watch?v=snVouw9bCq0).

- If you, like me, enjoy Tetris — and want to know a bit more about the randomization systems - here are a few articles: [How Tetris Randomizers Work (Bag, 7-Bag, Memoryless)](https://dinogame.gg/blog/how-tetris-randomizers-work/), [The history of Tetris randomizers](https://simon.lc/the-history-of-tetris-randomizers).

- [Pity System](https://www.g2a.com/news/glossary/what-is-a-pity-system-in-gaming-how-guaranteed-pulls-and-drop-protecti) was left out here, but it could be an excellent addition to make games that rely on randomness a bit more "fair."

- A nice way to see Poisson-Disc Sampling working is in the [Jason Davies](https://www.jasondavies.com/) project [here](https://www.jasondavies.com/poisson-disc/).

- The article [Fast Object Distribution](https://www.cs.cmu.edu/~ajw/s2007/0312-ObjectDistribution.pdf) does an excellent job of explaining the use of Halton sequences in games.

- Check that nice [article](https://excaliburjs.com/blog/Wave%20Function%20Collapse) about the WFC technique.

</br>
<p align="center">
<a href="https://github.com/thisaislan/unspaghettify/blob/home/README.md">
    prev</a>
    |
  <a href="https://github.com/thisaislan/unspaghettify/blob/index/README.md">
    index</a>
    |
  <a href="https://github.com/thisaislan/unspaghettify/blob/soon/README.md">
    next</a>
</p>
