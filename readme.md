# Discord in F#
Yep. Uses [Discord.Net.](https://github.com/discord-net/Discord.Net)

## How do F# and C# code line up?
### Awaiting an async task
---
```C#
// C#
await client.StartAsync();
```
```F#
// F#
client.StartAsync().Wait()
```

### Writing an asynchronous task
---
```C#
// C#
async Task MessageReceived(SocketMessage msg) { }
```
```F#
// F#
let MessageReceived(msg: SocketMessage) = task { }
```