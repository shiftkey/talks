# Branching Feels

Git doesn't have opinions on how you should work, so lots of other entities
have stepped in to fill this void. Here's some examples


## GitHub flow

[Source](http://scottchacon.com/2011/08/31/github-flow.html)

With this flow, `master` becomes the focus of activity and contributors create branches off `master`. This really comes into it's own with a code review tool so that everyone can help give feedback to changes:

![](https://camo.githubusercontent.com/3369c55fcbc0ad597bc1dfd9a8eac22f15d37f9e/68747470733a2f2f7777772e657665726e6f74652e636f6d2f73686172642f733139382f73682f61623938383935372d306132372d343637342d396538312d6464623539666234663263382f63636537323565646235646163373838316163633962633964623262643236612f646565702f302f5061737465642d496d6167652d323031342d30392d31372d31392d30322e706e67)

**Pros**

 - simple to follow
 - great for working on focused features

**Cons**

 - high churn codebases can add pull/merge overhead
 - requires keeping `master` deployable

## nvie's "git flow"

[Source](http://nvie.com/posts/a-successful-git-branching-model/)

**Pros**

 - more helpful for traditional projects
 - helps teams and contributors with long-running work

**Cons**

 - complexity with targeting code
 - merge pain is deferred

![](http://nvie.com/img/git-model@2x.png)

##  ZeroMQ's "release fork"

[Source](http://hintjens.com/blog:24)

This was an interesting approach brought to my attention recently, and I thought I'd mention it here:

**Pros**

 - better separation for maintenance
 - favourable for focused projects

**Cons**

 - not a mainstream approach
 - targeting fixes to multiple repositories

![](https://cloud.githubusercontent.com/assets/359239/9109221/4d27712a-3c77-11e5-9d3f-f7a3e7805ca9.png)

## One Repository To Rule Them All?

There's two main schools of thought with how to organise your repositories - the monorepo and repo-per-project. There's tradeoffs to each, so here's some talking points:

### Monorepos

Facebook and Google are some companies that work in this way and they tout it's friendliness to developers.

**Pros**

 - clone one repo to get *all* the code
 - commit can span multiple projects (easier to trace updating dependency)

**Cons**

 - having to extend VCS tooling to deal with limitations at scale (e.g. [Facebook scaling Mercurial](https://code.facebook.com/posts/218678814984400/scaling-mercurial-at-facebook/))
 - deployment becomes the pain point


### Repo-per-project

This is how we work at GitHub

**Pros**

 - clone only the projects you need
 -

**Cons**

 - having to extend VCS tooling to deal with limitations at scale (e.g. [Facebook scaling Mercurial](https://code.facebook.com/posts/218678814984400/scaling-mercurial-at-facebook/))
 - deployment becomes the pain point



## git merge --ff-only
