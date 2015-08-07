# Understanding `cherry-pick` and `rebase`

Consider these two branches:

```
          A---B---C---D---E @robrix
         /
    F---G---H---I master
            \
            J---K shiftkey
                ^
```



Let's say that commit `A` was actually a bugfix, which I want to incorporate
directly onto my branch - because I need it and I'm not sure when this will land.

There's other ways that I could achieve this:

 - rebase my branch on top of theirs
 - merge their branch into mine

And these are both terrible, because @robrix is a machine and has done a whole
bunch of other work that I don't really care about (or want t)

So we're left with `git cherry-pick A` to pull this in. But let's talk through
the process:

 - Git adds a file named `.git/CHERRY_PICK_HEAD` to the repository, containing
 the SHA you want to target to this branch
 - Git adds a file named `.git/COMMIT_EDITMSG` which contains the original
 commit message
 - Git generates a diff between `A` and `G` - think of this as basically a
 patch - let's call it `A*`
 - Git applies this patch to `HEAD`, which is currently `K`
   - if the patch applies cleanly, Git can create the commit automatically
   - if the patch doesn't apply cleanly, that means we have a conflict. Git
   updates the `COMMIT_EDITMSG` to indicate what files are conflicted
   (commented out). This often occurs when the current working tree has changed
   significantly and Git cannot resolve the differences.
   Once the user has resolved the conflicts they can complete the process by
   doing `git commit` - which let's you edit the original commit message if you
   feel like it.

Once you're done with the cherry-pick, this is the history you have:

```
             A---B---C---D---E @robrix
            /
       F---G---H---I master
               \
               J---K---L shiftkey
                       ^
```

Notice how I have a completely different commit on this branch? That's because
the commit hash is different - it might have been the same changes, applied
cleanly, from the same author, but Git cares about more than the commit contents
when it creates a commit.

In particular, I get a new hash here because the parent is `K`, not `G`.

Let's consider a different scenario:

```
          A---B---C---D---E @shiftkey
         /
    F---G---H---I master
                ^
```

I could do this:

```
> git cherry-pick A
> git cherry-pick B
> git cherry-pick C
> git cherry-pick D
```

Which means I cherry-pick `A` onto `master`:

```
          A---B---C---D @shiftkey
         /
    F---G---H---I master
                \
                J
```

And then cherry-pick `B`:

```
          A---B---C---D @shiftkey
         /
    F---G---H---I master
                \
                J---K
```

And then `C`:

```
          A---B---C---D @shiftkey
         /
    F---G---H---I master
                \
                J---K---L
```

And then `D`:

```
          A---B---C---D @shiftkey
         /
    F---G---H---I master
                \
                J---K---L---M
```

So my graph essentially looks like this.

What if I update my ref to point to the tip of my new branch `M`? It's almost
the same code, it just has a different base:

```
          A---B---C---D
         /
    F---G---H---I master
                \
                J---K---L---M @shiftkey
```

Congratulations, you've done a `rebase`!

```
> git rebase master @shiftkey
```

And at some point, you'll see the old commits be dereferenced and
garbage-collected, so your graph then looks like this:

```
    F---G---H---I master
                \
                J---K---L---M @shiftkey
```
