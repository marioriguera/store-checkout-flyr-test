# Store Checkout Solution

## Task Description

<p align="justify">
    You are the lead programmer for a small chain of supermarkets. You are required to make a
    simple cashier function that adds products to a cart and displays the total price.
    You have the following test products registered:
</p>

| Product code  | Name          | Price     |
|:--------------|:--------------|:----------|
| GR1           | Green tea     | £3.11     |
| SR1           | Strawberries  | £5.00     |
| CF1           | Coffee        | £11.23    |

Special conditions:

- The CEO is a big fan of buy-one-get-one-free offers and of green tea. He wants us to add a rule to do this.
- The COO, though, likes low prices and wants people buying strawberries to get a price discount for bulk purchases. If you buy 3 or more strawberries, the price should drop to £4.50
- The CTO is a coffee addict. If you buy 3 or more coffees, the price of all coffees should drop to two thirds of the original price.

<p align="justify">
    Our check-out can scan items in any order, and because the CEO and COO change   their minds often, it needs to be flexible regarding our pricing rules. The   interface to our checkout looks like this (shown in .NET Framework):
</p>

```csharp
var co = new Checkout(pricing_rules);
co.scan(item);
co.scan(item);
var price = checkout.total();
```

<p align="justify">
    There is no “right” way of solving this problem. There are many different ways  of solving it. We are interested in seeing your real developer “you” and how     you think. In order to complete this assignment, please return a github     repository within 5 natural days after receiving the test.
</p>
