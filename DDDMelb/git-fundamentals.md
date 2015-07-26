# Git Fundamentals

## Summary

> If you're brand new to Git, or just getting started, this is the workshop for you. We're going to walk through getting started with Git and learn some of the core commands that you need to be productive. No version control experience necessary.
>
> You should bring a laptop with Git installed, so you can follow along.

## Walkthrough

Git is a distributed version control system. But what does that even mean?

The goal for this workshop is to answer this question:

> How do I create some changes and merge them into a branch?

So let's start from the beginning.

```
git init my-first-repo
cd my-first-repo
```

I've now created a local repository - however it's just a blank canvas.

Of course, this probably isn't how you've experienced Git. You may have cloned
a repository from somewhere like GitHub or BitBucket, in which case you would
have a repository with the history pre-populated.

Rather than do that, what we're going to do in this session is build up a
repository from first principles.

So let's add a file to the repository's directory:

```
touch README.md
git status
```

> On branch master
>
> Initial commit
>
> Untracked files:
>   (use "git add <file>..." to include in what will be committed)
>
>        README.md

`status` is your go-to command when you want to check the state of your
repository. Typically you'll use it to see what changes are currently in your
working directory, but it will also tell you some extra details in other
scenarios:

 - when you are tracking changes on a remote branch
 - when you are in the middle of a long-running task like a complex merge
 - ???

So we have this empty file which isn't tracked by Git. Open that file in your
favourite editor and write some text into the file that we're going to use as
part of our first commit.

Git requires the user to stage the file before making a commit. So let's do that:

```
git add README.md
git status
```

Notice how `git status` is now showing a different message:

> On branch master
>
> Initial commit
>
> Changes to be committed:
>   (use "git rm --cached <file>..." to unstage)
>
>         new file:   README.md

Remember how I said `status` is useful? This is because it not only gives you
details but often also includes the command to run if you've made a mistake
somewhere along the way.


 - add
 - commit
 - log
 - checkout
 - merge
