# Git Fundamentals

## Summary

> If you're brand new to Git, or just getting started, this is the workshop for you. We're going to walk through getting started with Git and learn some of the core commands that you need to be productive. No version control experience necessary.
>
> You should bring a laptop with Git installed, so you can follow along.

## Guide

Most of the instructions in this workshop represent command line actions.

These are prefixed with the `>` character. Often these commands will also
display output, which is displayed without a prefix.

```
> git --version
git version 1.9.5.github.0
```

## Walkthrough

Git is a distributed version control system. But what does that even mean?

The goal for this workshop is to answer this question:

> How do I create some changes and merge them into a branch?

So let's start from the beginning.

```
> git init my-first-repo
Initialized empty Git repository in C:/code/my-first-repo/.git/
> cd my-first-repo
```

I've now created a local repository - however it's just a blank canvas.

Of course, this probably isn't how you've experienced Git. You may have cloned
a repository from somewhere like GitHub or BitBucket, in which case you would
have a repository with the history pre-populated.

Rather than do that, what we're going to do in this session is build up a
repository from first principles.

So let's add a file to the repository's directory:

```
> touch README.md
> git status
On branch master

Initial commit

Untracked files:
  (use "git add <file>..." to include in what will be committed)

       README.md
```

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
> git add README.md
> git status
On branch master

 Initial commit

 Changes to be committed:
   (use "git rm --cached <file>..." to unstage)

         new file:   README.md
```

Notice how `git status` is now showing a different message:

Remember how I said `status` is useful? This is because it not only gives you
details but often also includes the command to run if you've made a mistake
somewhere along the way.

So we're happy with our initial file, so let's commit this to the repository.

Git requires a message to describe each commit, so let's do that:

```
> git commit -m "first commit!"
[master (root-commit) ef49944] first commit!
 1 file changed, 1 insertion(+)
 create mode 100644 README.md
```

*Note: If we don't specify a message here, Git will launch your default text editor,
so you can do multi-line commits. And you should do this, because bad commit
messages make future programmers hate you. But I'm lazy.*

So with that done, what's the status of our repository?

```
> git status
On branch master
nothing to commit, working directory clean
```

Add another line with some words to the README, and check the status again:

```
> git status
On branch master
Changes not staged for commit:
  (use "git add <file>..." to update what will be committed)
  (use "git checkout -- <file>..." to discard changes in working directory)

	modified:   README.md

no changes added to commit (use "git add" and/or "git commit -a")
```

Notice how we're seeing a slightly different message here compared to before?
That's because since we committed the README, it's changes are being treated
differently by Git when looking at the status of the repository.

So let's stage this change and commit it:

```
> git add README.md
> git commit -m "updated the README"
[master 11d1834] updated the README
 1 file changed, 2 insertions(+)
```

So over time, as we continue to work on the files in our repository, we build up this long history of commits.

At any time, you can check the history for your current branch using `log`:

```
> git log
commit 11d1834b7d9e5ea86d4a886c04e44b8bd210d284
Author: Brendan Forster <brendan@github.com>
Date:   Sat Aug 1 14:07:00 2015 +0930

    updated the README

commit ef499444a594b1d8feeb5698a82d5ee042078d9b
Author: Brendan Forster <brendan@github.com>
Date:   Sat Aug 1 13:58:46 2015 +0930

    first commit!
```

This is a great way to look back at your history. But that's rather verbose - so let's simplify it a bit:

```
> git log --oneline
11d1834 updated the README
ef49944 first
```

There's a whole bunch of additional parameters to pass into log - here's a good example of what you can do:

https://coderwall.com/p/euwpig/a-better-git-log

Of course, making commits in this way is rather boring. And everyone talks about these "branches" things in Git. So let's go and create one of those.




 - checkout
 - merge
