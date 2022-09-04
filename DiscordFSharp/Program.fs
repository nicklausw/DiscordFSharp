open Discord
open Discord.WebSocket
open System.Threading.Tasks

let _client = new DiscordSocketClient()

// run when logged into discord
let readyTask = task {
  printfn "connected to discord"
}

// run when message received
let messageReceived(msg: SocketMessage) = task {
  // don't process this bot's messages
  if msg.Author.Id <> _client.CurrentUser.Id then
    printfn $"got a message: {msg.Content}"
    msg.Channel.SendMessageAsync("Got the message").Wait()
}

// our main async function
let MainAsync() =
  async {
    let token = System.Environment.GetEnvironmentVariable("TEST_BOT")

    // think of Wait() as equal to C# await
    _client.LoginAsync(TokenType.Bot, token).Wait()
    _client.StartAsync().Wait()
    
    // add our event functions
    _client.add_Ready(fun _ -> readyTask)
    _client.add_MessageReceived(fun msg -> messageReceived(msg))

    // keep running
    Task.Delay(-1).Wait()
  }

[<EntryPoint>]
let main argv =
  MainAsync() |> Async.RunSynchronously
  0

