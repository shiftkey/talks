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

> **Editor's Note**
> If we don't specify a message here, Git will launch your default text editor,
> so you can create multi-line commits. And you should do this, because bad
> commit messages make future programmers hate you. But I'm lazy.

Git uses SHA-1 as identifiers for it's objects. For our commit, it uses various
pieces of information to generate this unique identifier:

 - commit contents
 - committer and reviewer details
 - parent commit SHA
 - timestamp

If any of these values change, a different commit SHA is created.

> **Editor's Note**
> If you're feeling a bit uncomfortable about SHA collisions or not using
> SHA-256, here's a [quote](http://git-scm.com/book/en/v2/Git-Tools-Revision-Selection)
> to put you at ease:
>
> *"If all 6.5 billion humans on Earth were programming, and every second, each
> one was producing code that was the equivalent of the entire Linux kernel
> history (3.6 million Git objects) and pushing it into one enormous Git
> repository, it would take roughly 2 years until that repository contained
> enough objects to have a 50% probability of a single SHA-1 object collision.
> A higher probability exists that every member of your programming team will
> be attacked and killed by wolves in unrelated incidents on the same night."*
>

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

So over time, as we continue to work on the files in our repository, we build up
this long history of commits.

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

This is a great way to look back at your history. But that's rather verbose - so
let's simplify it a bit:

```
> git log --oneline
11d1834 updated the README
ef49944 first
```

There's a whole bunch of additional parameters to pass into log - here's a good
example of what you can do:

https://coderwall.com/p/euwpig/a-better-git-log

### Pushing and Pulling Changes

So we're making commits in our little repository, but how do we collaborate with
other people?

Git uses "remotes" to represent other repositories in your network. It uses the
terms `push` and `pull` to represent publishing and retrieving changes from
these other remotes.

If you have a repository hosted somewhere like GitHub, add it to your
repository:

```
> git remote add origin https://github.com/shiftkey/my-first-repo.git
```

I've set this up as an empty repository, so when I fetch the contents of this
repository you'll see that nothing happens:

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

> **Editor's Note**
> Here's a TL;DR of what's happening here:
>
> - Git worked out that it needed to send some objects to the remote repository -
>   because they don't exist there
> - these objects are compressed and sent to the remote repository
> - once those objects are created on the remote repository, the references on
>   the remote repository can be updated
> - because it didn't exist, Git also created the `master` branch on the remote
>   repository

A quick note about branches now we're pushing and pulling:

 - branch names can be different between repositories - it just means some
   gymnastics to configure your repo. I generally avoid this, and recommend
   beginners don't fight this convention.
 - when you push a change to a branch, Git will confirm the push is safe to do.
   If it's not safe, it will spit out an error about how things need to be
   reconciled. I'll explain this later.

When we're working with remote branches, we should ensure we're tracking these
changes:

```
> git branch -u origin/master
Branch master set up to track remote branch master from origin.
> git status
On branch master
Your branch is up-to-date with 'origin/master'.
nothing to commit, working directory clean
```

I'm going to make a commit on GitHub and then pull that into this branch.

Now, we can just fetch to see what's changed:

```
> git fetch
remote: Counting objects: 3, done.
remote: Compressing objects: 100% (2/2), done.
remote: Total 3 (delta 0), reused 0 (delta 0), pack-reused 0
Unpacking objects: 100% (3/3), done.
From https://github.com/shiftkey/my-first-repo
   11d1834..25da24c  master     -> origin/master
> git status
On branch master
Your branch is behind 'origin/master' by 1 commit, and can be fast-forwarded.
  (use "git pull" to update your local branch)
nothing to commit, working directory clean
```

See here how there's a new commit on the remote repository's `master` branch?
But we're haven't applied this change to our local branch?

Let's do that:

```
> git pull origin master
From https://github.com/shiftkey/my-first-repo
 * branch            master     -> FETCH_HEAD
Updating 11d1834..25da24c
Fast-forward
 README.md | 2 ++
 1 file changed, 2 insertions(+)
> git status
On branch master
Your branch is up-to-date with 'origin/master'.
nothing to commit, working directory clean
```

### Branching

We've gotten this far without even creating a branch. How dumb is that? Everyone
talks about these "branches" things in Git.

So let's go and create one of those:

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

Before continuing on to the next section, let's make one last change in the
README. I'm going to change the first line of the file to:

> THIS IS THE MASTER BRANCH, YO

I'm deliberately trying to trigger a merge conflict so we can see how this
works.

```
> git add README.md
> git commit -m "changed first line"
[master af8301b] changed first line
 1 file changed, 1 insertion(+), 1 deletion(-)
```

Let's go back to our feature branch:

```
> git checkout my-cool-feature
Switched to branch 'my-cool-feature'
```

And let's edit the first line to be something else:

> I AM ON THE FEATURE BRANCH

And let's commit this change:

```
> git add README.md
> git commit -m "changed first line to something else"
[master b35db61] changed first line to something else
 1 file changed, 1 insertion(+), 1 deletion(-)
```

Now, let's say our work is done and we need to merge this back into the `master`
branch.

```
> git checkout master
Switched to branch 'master'
Your branch is ahead of 'origin/master' by 1 commit.
  (use "git push" to publish your local commits)
> git merge my-cool-feature
Auto-merging README.md
CONFLICT (content): Merge conflict in README.md
Automatic merge failed; fix conflicts and then commit the result.
```

Amd there's our merge conflict! Let's have a look at the status:

```
> git status
On branch master
Your branch is ahead of 'origin/master' by 1 commit.
  (use "git push" to publish your local commits)
You have unmerged paths.
  (fix conflicts and run "git commit")

Unmerged paths:
  (use "git add <file>..." to mark resolution)

	both modified:   README.md

no changes added to commit (use "git add" and/or "git commit -a")
```
