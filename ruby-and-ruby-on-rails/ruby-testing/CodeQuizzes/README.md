# Fav solutions
* 01_03 for being simple, but effective
```ruby
class String
  def palindrome?
    self == self.reverse
  end
end
```
* 01_04 for =~ operator usage
```ruby
def vowels(string)
  string.split.select do |word|
    word =~ /[aeiou]/ # =~ checks if there's match against regex
  end
end
```
* 01_13 for getting to know format function
```ruby
class Float
  def to_money
    format("$%.2f", self)
  end
end
```
