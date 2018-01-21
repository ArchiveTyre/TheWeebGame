package the.weeb.game

import java.net.{Socket, ServerSocket, SocketException}
import java.io.{BufferedReader, InputStreamReader, PrintWriter};

class LoginServer(port: Int) extends Runnable {
  val serverSocket = new ServerSocket(port)
  
  override def run() {
    while(!serverSocket.isClosed()) {
      try {
        println("[LoginServer] Waiting to accept a socket!")
        val socket = serverSocket.accept()
        println("[LoginServer] Socket accepted")
        new Thread(new LoginHandler(socket)).start()
      }
      catch {
        case e: SocketException =>
      }
    }
    println("[LoginServer] Shutting down...")
  }
  
  def shutdown() {
    serverSocket.close
  }
}

class LoginHandler(val socket: Socket) extends Runnable {
  val clientInput = new BufferedReader(new InputStreamReader(socket.getInputStream))
  val clientOutput = new PrintWriter(socket.getOutputStream)
  
  override def run() {
    var input: String = clientInput.readLine()
    while(input != null) {
      if (input.startsWith("LOGIN ")) { 
        println("Trying to login!")
        clientOutput.println("Hello World!");
      }
      else {
        println("ERROR! Unknown command: " + input)
        clientOutput.println("ERROR!");
      }
      clientOutput.flush()
      input = clientInput.readLine()
    }
  }
}