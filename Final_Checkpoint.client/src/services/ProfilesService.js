import { AppState } from "../AppState.js"
import { Profile } from "../models/Account.js"
import { api } from "./AxiosService.js"

class ProfilesService {
  async getProfile(profileId) {
    const res = await api.get(`api/profiles/${profileId}`)
    AppState.profile = new Profile(res.data)
  }
}

export const profilesService = new ProfilesService()