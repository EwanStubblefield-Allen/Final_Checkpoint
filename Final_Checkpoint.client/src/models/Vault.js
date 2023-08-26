import { Profile } from "./Account.js"
import { RepoItem } from "./RepoItem.js"

export class Vault extends RepoItem {
  constructor(data) {
    super(data)
    this.name = data.name
    this.description = data.description
    this.img = data.img
    this.isPrivate = data.isPrivate
    this.creatorId = data.creatorId
    this.creator = new Profile(data.creator)
  }
}