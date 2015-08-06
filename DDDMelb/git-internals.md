# Git Internals

To walk through the Git internals, let's work with Git's plumbing to commit a
change to a file.

This is the test repository I'm working with:

```
> git clone https://github.com/shiftkey/fun-with-internals.git
Cloning into 'fun-with-internals'...
remote: Counting objects: 5, done.
remote: Compressing objects: 100% (3/3), done.
remote: Total 5 (delta 0), reused 5 (delta 0), pack-reused 0
Unpacking objects: 100% (5/5), done.
Checking connectivity... done.
> cd fun-with-internals
```

## Blobs

Blobs (binary large object files) are the content of files in a repository.
These are kinda boring, but also very essential.

Blobs in Git each have an identifier, which is just the hash of their contents:

```
> git hash-object README.md
6b2876824aa1d5a7ce70d9b182f239ee332460a9
```

So we have a relative path, some contents and the hash of the contents.

Let's make a change to the README and run it again:

```
> git hash-object README.md
ea78341ae4ff38abc206d49a51ed4cc2579805f7
```

Great, our content change is picked up as a new hash.

If you have a git-enhanced prompt you're probably seeing it has also detected
the change.

Anyway, let's check the status of the repository:

```
> git status
On branch master
Changes not staged for commit:
  (use "git add <file>..." to update what will be committed)
  (use "git checkout -- <file>..." to discard changes in working directory)

	modified:   README.md

no changes added to commit (use "git add" and/or "git commit -a")
```

At this point we'd have add this file and make a new commit. But let's not use
`add` here. Let's understand how Git detects this change:

Git has what's called an `index`. It represents the current state of your
working tree. You might have poked the contents of `.git/index` before, but
don't worry about that for now - it's an implementation detail.

So Git knows we've got a modified file on disk, but we can ask Git for the state
of the index:

```
> git ls-files --stage
100644 6b2876824aa1d5a7ce70d9b182f239ee332460a9 0	README.md
100644 27df989be2ec5c95cf626c02be47c7b5adbe35fa 0	docs/another-page.md
```

As We've not updated the index, we see the old hash for our README. We need
to update the index to point to our new README contents.

First we have to write our new blob to the object database:

```
> git hash-object -w README.md
ea78341ae4ff38abc206d49a51ed4cc2579805f7
```

At this point the object is orphaned - nothing links to it, so it could be
garbage collected in the future without affecting the integrity of the
repository.

We now tell the index to use our new blob for the README.md file:

```
> git update-index --add --cacheinfo 100644 ea78341ae4ff38abc206d49a51ed4cc2579805f7 README.md
```

So our index now reflects the commit we want to create:

```
> git ls-files --stage
100644 ea78341ae4ff38abc206d49a51ed4cc2579805f7 0	README.md
100644 27df989be2ec5c95cf626c02be47c7b5adbe35fa 0	docs/another-page.md
```

### A Note About Renames

To Git, renaming a file is equivalent to removing a file at path A and then
adding the file again at path B. However users may change the contents of the
file as part of this operation, which means you might not get the expected
result.

I highly recommend calling out the rename separate to any content changes,
make the rename an atomic change (don't mix it up with content).

## Trees

But what's a tree? To use a crude analogy, a tree is kinda like a folder:

 - A tree can contain blobs or other trees.
 - A folder can contain files or other folders.

Now we need to transform the contents of our index into a tree:

```
> git write-tree
6c253caf1b2034e45b7b24e9ac9a39deb3074fd3
```

And Git knows this is a tree:

```
> git cat-file -t 6c253caf1b2034e45b7b24e9ac9a39deb3074fd3
tree
```

## Commit

A commit requires a tree and some other pieces of data. I'm lazy, so let's use
the defaults which are configured for the repository.

At a minimum, we need a tree and a commit message. So let's do that:

```
> git commit-tree 6c253caf1b2034e45b7b24e9ac9a39deb3074fd3 -p HEAD -m "edited the README"
eb7cd9c46f0e16afc905a83647d794631a031dfc
```

This is the hash for our new commit. If I crack it open I can see that it's
added my details:

TODO: is this right?

```
> git show -p eb7cd9c46f0e16afc905a83647d794631a031dfc
tree 6c253caf1b2034e45b7b24e9ac9a39deb3074fd3
parent 91fd1bcb6e14d47faacb95231a942c963b24b4ce
author Brendan Forster <brendan@github.com> 1438837181 +1000
committer Brendan Forster <brendan@github.com> 1438837181 +1000

edited the README
```

## Refs

And the last piece of this puzzle is what Git calls `refs` - you can kinda think
of these as branches, but a more accurate analogy is a pointer to an address.

```
> git show HEAD
commit 91fd1bcb6e14d47faacb95231a942c963b24b4ce
Author: Brendan Forster <brendan@github.com>
Date:   Thu Aug 6 14:35:31 2015 +1000

    first!
...
```

See how HEAD still hasn't updated, even though we've made a new commit?

Wait, let's step back a sec. HEAD is just a shorthand for "where am I
currently?"

```
> less .git/HEAD
ref: refs/heads/master
> less .git/refs/heads/master
91fd1bcb6e14d47faacb95231a942c963b24b4ce
```

Notice how `master` still points to my "first!" commit? That's what we need to
update.

If I have a remote repository those refs are tracked in a different "namespace".
So when I'm tracking my local `master` against the `master` of another
repository, Git is just comparing that ref to this ref and essentially walking
the graph of commits.

Let's set our `master` to be the new commit:

```
> git update-ref refs/heads/master eb7cd9c46f0e16afc905a83647d794631a031dfc
> git show HEAD
commit eb7cd9c46f0e16afc905a83647d794631a031dfc
Author: Brendan Forster <brendan@github.com>
Date:   Thu Aug 6 14:59:41 2015 +1000

    edited the README
...
```

And if we had permissions to the repo, we can push this ref to update the history
on the other repository:

```
> git push origin master
...
```
