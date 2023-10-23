## ⚔️ Tyrion

[![Build Status](https://dev.azure.com/lucasluizss/Tyrion.CQRS/_apis/build/status/lucasluizss.Tyrion.CQRS?branchName=master)](https://dev.azure.com/lucasluizss/Tyrion.CQRS/_build/latest?definitionId=1&branchName=master)
[![GitHub license](https://img.shields.io/github/license/lucasluizss/Tyrion.CQRS)](https://github.com/lucasluizss/Tyrion.CQRS)
[![NuGet](https://img.shields.io/nuget/dt/tyrion.svg)](https://www.nuget.org/packages/tyrion)
[![NuGet](https://img.shields.io/nuget/vpre/tyrion.svg)](https://www.nuget.org/packages/tyrion)
[![Codacy Badge](https://api.codacy.com/project/badge/Grade/1e18174734fa415a9e64ef831e87d4b4)](https://www.codacy.com/manual/lucasluizss/Tyrion.CQRS?utm_source=github.com&utm_medium=referral&utm_content=lucasluizss/Tyrion.CQRS&utm_campaign=Badge_Grade)

Tyrion is an implementation of mediator pattern for dotnet

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

In your Startup.cs, add Tyrion on your service with some typeof class, for identify the currently assembly project. (Could be any class in your project) like this:

```csharp

services.AddTyrion(typeof(Category));

```

When we talk about mediator everything is about requests... Tyrion is abstract for just one interface, where you will handle with multiple concepts types implementing just the IRequest interface for Commands and Queries and INotification interface when you want send multiple notifications events.

You must keep it simple, so when you are creating a CommandHandle you have to combine your Command request. That way, Tyrion can find quickly the required implementation.

Samples of requests bellow:

```csharp
public class ProductCommand : IRequest { }

public class CategoryQuery : IRequest { }

public class CategoryAddedEvent : INotification { }
```

Samples of validation requests bellow (this is an optional implementation):

```csharp
public sealed class CategoryCommandValidator : Validator<CategoryCommand>
{
	public CategoryCommandValidator()
	{
		RuleFor(x => x.Name).NotEmpty();
	}
}
```

Samples of Handlers bellow:

```csharp
public sealed class CategoryCommandHandler : IRequestHandler<CategoryCommand, Category>
{
	public async Task<IResult<Category>> Execute(CategoryCommand command)
	{
		return await Result<Category>.SuccessedAsync(new Category());
	}
}

public sealed class CategoryQueryHandler : IRequestHandler<CategoryQuery, Category>
{
	public async Task<IResult<Category>> Execute(CategoryQuery request)
	{
		return await Result<Category>.SuccessedAsync(new Category());
	}
}

public sealed class CategoryNotificationHandler : INotificationHandler<CategoryAddedEvent>
{
	public async Task Publish(CategoryAddedEvent notification)
	{
		await Task.CompletedTask;
	}
}
```

IRequestHandler an receive one or two generics arguments, the first one is your request (Command, Query, etc) all implementing IRequest and the secound one is you return type if you need.

Case you implement your request validation (using Validator<> class), Tyrion will handle with the validation and return your errors automaticaly.

That way we can assunrency that we will receive what we are requiring.

Tyrion allows you to implement multiple handlers in your application service. Just in case you need keep the same context together.

Something like this:

```csharp
public sealed class ProductCommandHandler : IRequestHandler<SaveProductCommand, Product>,
					    IRequestHandler<UpdateProductCommand, Product>,
					    IRequestHandler<RemoveProductCommand>,
					    IRequestHandler<InativeProductCommand>
{
	public async Task<IResult<Product>> Execute(SaveProductCommand request)
	{
		return await Result<Product>.SuccessedAsync(new Product());
	}

	public async Task<IResult<Product>> Execute(UpdateProductCommand command)
	{
		return await Result<Product>.SuccessedAsync(new Product());
	}

	public async Task<IResult> Execute(RemoveProductCommand request)
	{
		return await Result.SuccessedAsync(new Product());
	}

	public async Task<IResult> Execute(InativeProductCommand request)
	{
		return await Result.SuccessedAsync();
	}
}
```

I hope you enjoy this. I will work to include little improvements!

## 🙋🏽‍♂️ Author

X: [@lucasluizss](https://x.com/lucasluizss)
Platform: [@lucaslab.dev](https://lucaslab.dev)

## 📝 Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## ⚖️ License

[MIT](https://choosealicense.com/licenses/mit/)
