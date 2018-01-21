package the.weeb.game

object Main {
  def main(args: Array[String]): Unit = {
    val loginServer = new LoginServer(9030)
    new Thread(loginServer).start()
    println("[Main] Server started!")
    io.StdIn.readLine()
    loginServer.shutdown
    println("Shut down!")
  }
}