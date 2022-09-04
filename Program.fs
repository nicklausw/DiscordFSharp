open Discord
open Discord.WebSocket
open System.Threading.Tasks

let client = new DiscordSocketClient()

// run when logged into discord
let readyTask() = task {
  printfn "connected to discord"
}

// run when message received
let messageReceived(msg: SocketMessage) = task {
  // don't process this bot's messages
  if msg.Author.Id <> client.CurrentUser.Id then
    printfn $"got a message: {msg.Content}"
    msg.Channel.SendMessageAsync("Got the message").Wait()
}

// our main async function
let MainAsync() = task {
  let token = System.Environment.GetEnvironmentVariable("TEST_BOT")

  // log in
  client.LoginAsync(TokenType.Bot, token).Wait()
  client.StartAsync().Wait()
    
  // add our event functions
  client.add_Ready(fun _ -> readyTask())
  client.add_MessageReceived(fun msg -> messageReceived(msg))

  // keep running
  Task.Delay(-1).Wait()
}

[<EntryPoint>]
let main argv =
  MainAsync().Wait()
  0

