# HexClient

**HexClient** is an open-source alternative to the official League of Legends client. Built using the modern [Avalonia UI](https://avaloniaui.net/) framework, HexClient aims to be a lightweight, fast, and responsive launcher for (addicted) League players.

---

### Why would I use HexClient rather than League original client ?

We noticed that the official League client is prone to bugs and lag because of **how it's built**: it's essentially a stack of **multiple plugins layered on top of each other**. Every time you start a game (and the client closes and reopens), all those plugins are reloaded. It’s similar to **opening a new browser tab** with tons of extensions and scripts running every time you enter or leave a game. By the end of a long session, the official client becomes increasingly slow and unresponsive.

**HexClient is different.**  
We **don’t use embedded plugins** or any form of plugin stacking. Everything is integrated natively, ensuring that performance remains consistent regardless of session length. That means less memory usage, faster loading, and a smoother overall experience. (We just don't aim for the good visuals of the original client)

---

## How to play using HexClient
This version does not have a usable binary yet, we are working on it, please wait. :)

## Getting Started for Developpers

To build and run HexClient:

1. Ensure you have [.NET 9](https://dotnet.microsoft.com/en-us/download/dotnet/9.0) installed
2. Clone this repository
3. Run the solution using your favorite IDE (e.g. Rider, Visual Studio, ...)
4. Launch the client from the `HexClientProject` folder

```bash
git clone https://github.com/YOUR_USERNAME/HexClient.git
cd HexClient
dotnet build
dotnet run --project HexClientProject
```

## Community and Contributions
We’re open to all contributors who share our passion for the game.
Feel free to open issues, suggest features, or submit pull requests.

## About Us
HexClient was born from the desire to enhance the player experience by building a launcher that is:

- **Lightweight**  
- **Fast**  
- **Unobtrusive**  
- **Customizable**

I love League of Legends and am not affiliated with Riot Games in any way. This is a **non-commercial, open-source project** and is created purely for learning and fun. We **do not condone cheating, data scraping beyond what Riot allows**, or any activity that would violate the [Riot Games Terms of Service](https://www.riotgames.com/en/terms-of-service). We want to be **transparent, respectful**, and **never want anyone to be banned** due to our launcher.

Vincent – Project lead, MVVM architecture wizard, client optimisation

---

## License

This project is licensed under the [MIT License](LICENSE).  
You are free to use, copy, modify, merge, publish, and distribute the software—provided that you include proper attribution and keep the license in your version.

Please note:
- This project is **not affiliated with or endorsed by Riot Games, Inc.**  
- All **trademarks, service marks, and game assets** related to League of Legends are the property of Riot Games.
- Use responsibly and at your own risk.

---

We are doing this on our free time and we welcome feedback, ideas, or even just encouragement. Thanks for checking out HexClient!

Thanks for the contributors that helped for the developement.
