# <h2 align=center>*LAN Messenger*</h2>


<p align="center">
   <img src="https://github.com/edin-aj/LANMessanger/blob/main/LANMessanger/Resources/lan_messanger.png?raw=true" width="20%">
</p>

I built this a while back as a chat app for local networks, using C# and Windows Forms. There is no server: every computer talks directly to the others over UDP. It's a simple project, but it covers a good part of network programming: broadcast messages, finding other users, a small custom protocol, and a chat window that updates live.

*This project is old and no longer maintained. It was a personal learning project and I won't be fixing bugs or adding features, but feel free to fork or build on it.*

*And yes, "Messanger" is spelled wrong. I noticed it too late, so I kept the name as it is with the rest of the project, its a shame, but it is what it is :sweat_smile:*

## <h2 align=center>*How it works*</h2>

When you start the app, you type a name and connect. The name must be one word, with only letters and digits. Before connecting, the app also checks that you are on a WiFi network, and warns you if your subnet mask may block communication.

There is no central server. Every client opens a UDP socket on port `27000` and sends its messages as a broadcast, so everyone on the network receives them. The messages are plain text and look like `COMMAND@arg1@arg2`, for example `MESSAGE@Edin@hello`.

When you join, your app asks the network *"who is online?"* and the other clients answer, so your user list gets filled. The app also checks if your name or IP is already in use, and if it is, the new user gets kicked out.

## <h2 align=center>*Features*</h2>

+ Group chat with colored messages and timestamps
+ Live list of active users, with a counter
+ Join and leave notifications
+ Typing indicator *(for example "Typing: Alice and Bob...")*
+ Poke a user from a custom-drawn menu
+ Search inside the chat for a whole word, with a separate results window that shows each match with the line before and after it
+ Protection against duplicate names and running the app twice on the same PC

## <h2 align=center>*Project structure*</h2>

| File | What it does |
|---|---|
| `Form1.cs` | Login screen: name checks and network checks |
| `ChatRoom.cs` | Main chat window, and where all messages are handled |
| `UDP.cs` | UDP socket: sends broadcasts and listens in the background |
| `Clients.cs` | Stores the name, port and IPv4 address of the local user |
| `SearchResult.cs` | Window for the chat search results |
| `Program.cs` | Entry point |

## <h2 align=center>*Requirements*</h2>

+ Windows
+ **.NET Framework 4.7.2**
+ **Visual Studio 2022** *(or any IDE that can build .NET Framework WinForms projects)*

To build it, open `LANMessanger.sln` in Visual Studio and press `F5`. To try the chat, run it on two or more computers in the same network.
Or you can download the ready-to-run version from the [Releases](https://github.com/edin-aj/LANMessanger/releases/tag/lanmessanger) page.

*Important: allow the app in Windows Firewall for **UDP port 27000**, or the other computers will not receive your messages.*

## <h2 align=center>*Known limitations*</h2>

I know this project is not perfect, and since it's not maintained, these will stay as they are:

+ There is **no encryption or login**. Anyone on the network can read or fake messages, so don't use it for private talks.
+ The `@` symbol is used to split messages, so a chat message with `@` inside gets cut.
+ It works only inside one subnet, because broadcasts don't pass through routers.
+ UDP can lose messages when the network is busy.
+ The chat search is case-sensitive and only finds whole words.
+ The UI is updated from a background thread without `Invoke`, which is not the safest way in WinForms.

## <h2 align=center>*What I learned*</h2>

+ Using UDP sockets and broadcast in C#
+ Designing a small text protocol without a server
+ Working with threads in WinForms *(a background listener that updates the UI)*
+ Building custom UI, like a responsive layout and an owner-drawn menu

## <h2 align=center>*License*</h2>

+ Source code is licensed under **MIT** — see [LICENSE](LICENSE).

## <h2 align=center>*Changelog:*</h2>

    2026 update:
        - Initial Release (old project, uploaded as-is)

<table align="center">
  <tr>
    <td align="center">
      <h2>Issues, pull requests and repo</h2>
      <a href="https://github.com/edin-aj/LANMessanger/issues">
        <img src="https://img.shields.io/github/issues/edin-aj/LANMessanger" alt="GitHub issues">
      </a>
      <a href="https://github.com/edin-aj/LANMessanger/issues?q=is%3Aissue+is%3Aclosed">
        <img src="https://img.shields.io/github/issues-closed/edin-aj/LANMessanger" alt="GitHub issues closed">
      </a>
      <a href="https://github.com/edin-aj/LANMessanger/pulls">
        <img src="https://img.shields.io/github/issues-pr/edin-aj/LANMessanger" alt="GitHub pull requests">
      </a>
      <a href="https://github.com/edin-aj/LANMessanger/pulls?q=is%3Apr+is%3Aclosed">
        <img src="https://img.shields.io/github/issues-pr-closed/edin-aj/LANMessanger" alt="GitHub pull requests closed">
      </a>
      <br>
      <a href="https://github.com/edin-aj/LANMessanger/network/members">
        <img src="https://img.shields.io/github/forks/edin-aj/LANMessanger?style=for-the-badge&color=lightgreen" alt="Forks">
      </a>
      <a href="https://github.com/edin-aj/LANMessanger/watchers">
        <img src="https://img.shields.io/github/watchers/edin-aj/LANMessanger?style=for-the-badge&color=lightgreen" alt="Watchers">
      </a>
      <a href="https://github.com/edin-aj/LANMessanger/commits/main">
        <img src="https://img.shields.io/github/last-commit/edin-aj/LANMessanger?style=for-the-badge&color=lightgreen" alt="Last Commit">
      </a>
      <br>
      <h2>You can give me a star!</h2>
      <a href="https://github.com/edin-aj/LANMessanger/stargazers">
        <img src="https://i.imgur.com/FyVXkZL.png" alt="Built with Love">
      </a>
    </td>
  </tr>
</table>
