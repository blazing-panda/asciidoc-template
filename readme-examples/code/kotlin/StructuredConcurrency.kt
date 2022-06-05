import kotlinx.coroutines.*

fun thread(): String = Thread.currentThread().name;

suspend fun worker(i: Int) {
    println("Worker $i started ${thread()}")
    delay(200)
    println("Worker $i completed ${thread()}")
}

suspend fun longRunningCalculation(i: Int): Int {
    println("Calculation $i started ${thread()}")
    delay(1000)
    return 69
}

suspend fun startCalculations() = coroutineScope {
    val res = async { longRunningCalculation(1) }
    println("not blocking... ${thread()}")
    println("Calculation completed: ${res.await()} ${thread()}")
}

fun main() {
    println("Synchronous World!  ${thread()}")

    runBlocking(Dispatchers.Default) {
        println("Concurrent world! ${thread()}")
        val job1 = launch { worker(1) }
        launch { startCalculations() }
        println("Job1 is completed? ${job1.isCompleted}")
        println("Coroutine completing ${thread()}")
    }

    println("Synchronous World!  ${thread()}")
}
/*  output:     Synchronous World!          main
                Concurrent world!           DefaultDispatcher-worker-2
                Job1 is completed? false
                Coroutine completing        DefaultDispatcher-worker-2
                Worker 1 started            DefaultDispatcher-worker-3
                not blocking...             DefaultDispatcher-worker-2
                Calculation 1 started       DefaultDispatcher-worker-2
                Worker 1 completed          DefaultDispatcher-worker-1
                Calculation completed: 69   DefaultDispatcher-worker-1
                Synchronous World!          main                                */



