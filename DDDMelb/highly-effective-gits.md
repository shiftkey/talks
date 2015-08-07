# The Seven Habits Of Highly Effective Gits

 - branch early and often

 - make small, atomic commits

 - give your commits descriptive messages

 - know the short-hand, HEAD, ~N, ^N

 - know `reset`, love `reset`

 ```
 > git reset HEAD .
 > git reset origin/master --hard
 > git reset other-branch --hard
 ```

 - use reflog

 - use `rebase` to tell a story
   - reorder commits
   - squash commits
   - drop commits
   - interactive rebase
