# ⚔️ Tyrion.CQRS

[![Build Status](https://lucasluizssdev.visualstudio.com/Tyrion.CQRS/_apis/build/status/lucasluizss.Tyrion.CQRS?branchName=master)](https://lucasluizssdev.visualstudio.com/Tyrion.CQRS/_build/latest?definitionId=1&branchName=master)

Tyrion is an implementation of mediator pattern for .Net Core
{ KISS }

## 👨🏽‍💻 Installing

- Package Manager
```bash
Install-Package Tyrion
```

- .Net CLI
```bash
dotnet add package Tyrion
```

- Pakat CLI
```bash
paket add Tyrion 
```

## 🧾 Usage

In your Startup.cs, add Tyrion in your service with some typeof class, for identify the currently assembly project. (Could be any class in your project) like this:

```csharp

services.AddTyrion(typeof(Stuff));

```

When we talk about mediator everything is about requests... Tyrion is abstract for just one interface, where you will handle with multiple concepts types implementing just the IRequest interface.

You must keep it simple, so when you are creating a CommandHandle you have to combine your Command request. That way, Tyrion can find quickly the required implementation.

Samples of requests bellow:

```csharp
public class SuffCommand : IRequest { }

public class StuffQuery : IRequest { }

public class StuffNotification : IRequest { }
```

Samples of validation requests bellow (this is an optional implementation):

```csharp
public sealed class StuffCommandValidator : Validator<StuffCommand>
{
    public StuffCommandValidator()
    {
        RuleFor(x => x.MyProperty).NotEmpty();
    }
}
```

Samples of Handlers bellow:

```csharp
public sealed class StuffCommandHandler : IRequestHandler<StuffCommand, Stuff>
{
    public async Task<IResult<Stuff>> Execute(StuffCommand request)
    {
        return await Task.FromResult(Result<Stuff>.Successed(new Stuff()));
    }
}

public sealed class StuffQueryHandler : IRequestHandler<StuffQuery, Stuff>
{
    public async Task<IResult<Stuff>> Execute(StuffQuery request)
    {
        return await Task.FromResult(Result<Stuff>.Successed(new Stuff()));
    }
}

public sealed class StuffNotificationHandler : IRequestHandler<StuffNotification, Stuff>
{
    public async Task<IResult<Stuff>> Execute(StuffNotification request)
    {
        return await Task.FromResult(Result<Stuff>.Successed(new Stuff()));
    }
}
```

IRequestHandler an receive one or two generics arguments, the first one is your request (Command, Query, Notification, etc) all implementing IRequest, and the secound one is you return type if you need.

Case you implement your request validation (using Validator<> class), Tyrion will handle with the validation and return your errors automaticaly.

That way we can assunrency that we will receive what we are requiring.

Tyrion allows you to implement multiple handlers in your application service. Just in case you need keep the same context together.

Something like this:

```csharp
public sealed class TestCommandHandler : IRequestHandler<TestCommand, Test>,
                                         IRequestHandler<Test1Command, Test1>,
                                         IRequestHandler<Test2Command, Test2>,
                                         IRequestHandler<Test3Command>,
{
    public async Task<IResult<Test>> Execute(TestCommand request)
    {
        return await Task.FromResult(Result<Test>.Successed(new Test()));
    }

    public async Task<IResult<Test1>> Execute(Test1Command command)
    {
        return await Task.FromResult(Result<Test1>.Successed(new Test1()));
    }

    public async Task<IResult<Test2>> Execute(Test2Command request)
    {
        return await Task.FromResult(Result<Test2>.Successed(new Test2()));
    }

    // This sample doesn't need a return
    public async Task Execute(Test3Command request)
    {
        await Task.CompletedTask;
    }
}
```

I hope you enjoy this. I will work to include little improvements!

## 🙋🏽‍♂️ Author

Twitter: [@lucasluizss](https://twitter.com/lucasluizss)

## 📝 Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## ⚖️ License
[MIT](https://choosealicense.com/licenses/mit/)
