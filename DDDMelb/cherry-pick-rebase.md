# Understanding `cherry-pick` and `rebase`

Consider these two branches:

```
          A---B---C---D---E robrix
         /
    F---G---H---I master
            \
            H---I shiftkey
```

Let's say that A was actually a bugfix, which I want to incorporate directly onto my branch - because I need it and I'm not sure when this will land.

There's other ways that I could achieve this:

 - rebase my branch on top of theirs
 - merge their branch into mine

And these are both terrible, because @robrix is a machine and has done a whole bunch of other work that I don't really care about (or want t)

So let's talk about
