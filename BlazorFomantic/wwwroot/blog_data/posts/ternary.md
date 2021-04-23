A Study in Ternary statements in C#
===
I've often been critisized for not preferring ternary statements when they are possible. For example, I would prefer:

```csharp
if (user.IsAdmin) 
{
    return UserAccess.SuperUser;
}

return GetRegularUserRights(user);
```

Over

```csharp
return user.IsAdmin
  ? UserAccess.SuperUser 
  : GetRegularUserRights(user);
```

It's not that I never use ternary ifs. They are useful for assignment (more on this later):

`var customerType = customer.previousPurchases == 0 ? CustomerType.NewCustomer : CustomerType.ExistingCustomer;`

### Harder to debug
I always find myself fumbling when debugging ternary statements. Instead of having the ability to have a breakpoint on each branch, the ternary statement is seen as a single statement. There is no Gate for the condition and so you have to evaluate both cases on a single breakpoint.

### Harder to read
This is one may be debatable, but I think there is an _objective_ answer, and I am staking my claim: they are in general harder to read. Part of that is because they are highly compressed, but also because `?` and `:` convey nothing special. In no other context do these symbols mean `true` and `false`. I always find myself spending more time reading and parsing these statements in my mind than I do with regular `if` statements. If you are unsure, observe the time you take with ternary statements as opposed to `if` statements. Maybe there is a difference in how I think versus others. However after programming for 20 years, I still find them frustrating to read, and _flow-breaking_.

### Harder to modify
Code bases change over time, and some are easier to modify than others. A ternary statement is usually ruined when a third condition enters the fray. For developers that prefer ternary statements, this is not a problem:

```csharp
return user.IsAdmin
  ? UserAccess.SuperUser 
  : user.IsTemporaryAuditor 
    ? UserAccess.TemporarySuperReader
    : GetRegularUserRights(user);
```
However I think this just exarcebates the problems I've highlighted with ternary expressions above. Debugging and reading become even more difficult.

### No core principle
There is no principle that says _"ternary expressions should be preferred"_. That leaves them in a terrible limbo, because there is no cut and dry rule. Sometimes they are useful in assignments, but in C# this use has waned with the `??` and `?.` operators.

### What about switch expressions?
```csharp
return switch user.IsAdmin
{
   true => UserAccess.SuperUser,
   false => GetRegularUserRights(user)
};
```
Compared to:

```csharp
return user.IsAdmin
  ? UserAccess.SuperUser 
  : GetRegularUserRights(user);
```

While the switch expression requires more syntax, it conveys its meaning much more directly than the ternary expression, and if the boolean changes to an enum, which is a common modification, then introducing more cases is easy.

### Let practicality drive your decisions
I used to use a lot of ternary expressions as a young inexperienced programmer, because I liked them aesthetically, but I was turned off of ternaries when I started working on large production code bases. Money haemorrhages as you sit there trying to figure out what's wrong, and it's frustrating more often than illuminating when you reach a ternary expression. In a large code base, you're often debugging someone else's code, and all those ternaries that you didn't write are somehow not as readable to you as your own. Primarily this is because code you didn't write has no recent or even distant memory to you about what it is intended to do, so you are forced to parse it completely. It's pretty easy to quickly read, parse and debug regular if statements, and if you need to do a fix in a hurry you can easily add another if statement, however ternaries don't afford the same luxuries, and nested ternaries are even worse.

In a simple boolean to enum assignment, I still use ternaries:

`var foodType = food.IsFruit ? FoodType.Fruit : FoodType.Unkown;`

However ternaries occupy the same place in my code that donuts do in my diet. I'll have one rarely and with careful consideration.

I've decided not to tell other developers to limit or stop using ternaries, and it's not something I will raise in a review unless it is particularly egregious. Unfortunately I couldn't find psychological studies that measure the readability of if statements vs ternary expressions, which is a shame. My hunch is however that the outcome will come out in favour of if statements most of the time. 