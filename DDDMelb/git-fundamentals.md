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

### Tracking Changes

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


### Committing a change

So we're happy with our initial file, let's commit this to the repository.

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

### Viewing History

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

### Pushing and Pulling Changes

So we're making commits in our little repository, but how do we collaborate with other people?

Git uses "remotes" to represent other repositories in your network. It uses the terms `push` and `pull` to represent publishing and retrieving changes from these other remotes.

If you have a repository hosted somewhere like GitHub, add it to your repository:

```
> git remote add origin https://github.com/shiftkey/my-first-repo.git
```

I've set this up as an empty repository, so when I fetch the contents of this repository you'll see that nothing happens:

```
> git fetch
```

Now I want to publish the current state of my `master` branch:

```
> git push origin master
Counting objects: 6, done.
Delta compression using up to 8 threads.
Compressing objects: 100% (2/2), done.
Writing objects: 100% (6/6), 492 bytes | 0 bytes/s, done.
Total 6 (delta 0), reused 0 (delta 0)
To https://github.com/shiftkey/my-first-repo.git
 * [new branch]      master -> master
```

This is definitely intimidating to see if you're not familiar with it, but here's a TL;DR: of what's happening here:

 - Git worked out that it needed to send some objects to the remote repository - because they don't exist there
 - these objects are compressed and sent to the remote repository
 - once those objects are created on the remote repository, the references on the remote repository can be updated
 - because it didn't exist, Git created the `master` branch on the remote repository

A quick note about branches now we're pushing and pulling:

 - branch names can be different between repositories - it just means some command gymnastics to manage this. I generally avoid this, and recommend beginners don't fight against these conventions
 - when you're pushing a branch name, Git will confirm the push makes sense. If it doesn't, it will spit out a lovely error, like this:

 





### Branching

Everyone talks about these "branches" things in Git. So let's go and create one of those:

```
> git checkout -b my-cool-feature
Switched to a new branch 'my-cool-feature'
```

`checkout` is how you can change your working directory to a specific commit.

Often, you'll use `checkout` to switch between branches:

```
> git checkout master
Switched to branch 'master'
```

Other times, you'll want to undo changes in your working directory:

```
> git checkout -- README.md
Switched to branch 'master'
```


 - merge
