# Steps to reproduce

Make sure that release build is working:

1. `docker-compose build && docker-compose up -d`
2. `./run-producer.sh`
3. `./run-consumer.sh`
4. Play with them.

Change the package version to `0.11.3-ci-303`, rebuild container (`docker-compose build`), try to run producer, get an exception:

````
$ ./run-producer.sh

docker-compose run dotnet dotnet Producer/bin/Release/netcoreapp2.0/Producer.dll kafka1:9092,kafka2:9092,kafka3:9092 test-topic
+ docker-compose run dotnet dotnet Producer/bin/Release/netcoreapp2.0/Producer.dll kafka1:9092,kafka2:9092,kafka3:9092 test-topic

Unhandled Exception: System.ArgumentException: Cannot bind to the target method because its signature or security transparency is not compatible with that of the delegate type.
   at System.Reflection.RuntimeMethodInfo.CreateDelegateInternal(Type delegateType, Object firstArgument, DelegateBindingFlags bindingFlags, StackCrawlMark& stackMark)
   at System.Reflection.RuntimeMethodInfo.CreateDelegate(Type delegateType)
   at Confluent.Kafka.Impl.LibRdKafka.SetDelegates(Type nativeMethodsClass)
   at Confluent.Kafka.Impl.LibRdKafka.Initialize(String userSpecifiedPath)
   at Confluent.Kafka.Producer..ctor(IEnumerable`1 config, Boolean manualPoll, Boolean disableDeliveryReports)
   at Confluent.Kafka.Producer`2..ctor(IEnumerable`1 config, ISerializer`1 keySerializer, ISerializer`1 valueSerializer, Boolean manualPoll, Boolean disableDeliveryReports)
   at AenSidhe.Kafka.Test.Producer.Program.<MainAsync>d__1.MoveNext() in /app/Producer/Program.cs:line 31
--- End of stack trace from previous location where exception was thrown ---
   at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw()
   at System.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccessAndDebuggerNotification(Task task)
   at AenSidhe.Kafka.Test.Producer.Program.Main(String[] args) in /app/Producer/Program.cs:line 25
````
