# The Seven Habits Of Highly Effective Gits

 - branch early and often

 - make small, atomic commits

 - give your commits descriptive messages

 - know the short-hand, HEAD, ~N, ^N

 - know `reset`, love `reset`

```
 # reset staging area
 > git reset HEAD .

 # instead of pulling (which implicitly hits the network)
 # you can achieve the same result in two steps
 > git fetch origin
 > git reset origin/master --hard

 # just move my ref to some other point - aka yolo it
 > git reset other-branch --hard
```

 - use reflog

 - use `rebase` to tell a story
   - reorder commits
   - squash commits
   - drop commits
   - interactive rebase
