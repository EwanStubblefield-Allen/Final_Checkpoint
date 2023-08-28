import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { api } from "./AxiosService.js"

class KeepsService {
  async getKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data.map(d => new Keep(d))
  }

  async getKeepById(keep) {
    keep.views++
    AppState.activeKeep = keep
    const res = await api.get(`api/keeps/${keep.id}`)
    AppState.activeKeep.kept = res.data.kept
  }

  async getKeepsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    AppState.keeps = res.data.map(d => new Keep(d))
  }

  async createKeep(keepData) {
    const res = await api.post('api/keeps', keepData)

    if ((!AppState.profile.id || AppState.profile.id == AppState.account.id) && !AppState.activeVault) {
      AppState.keeps.push(new Keep(res.data))
    }
  }

  async updateKeep(keepData) {
    const res = await api.put(`api/keeps/${keepData.id}`, keepData)
    const foundIndex = AppState.keeps.findIndex(k => k.id == res.data.id)
    AppState.keeps.splice(foundIndex, 1, new Keep(res.data))
  }

  async removeKeep(keepId) {
    await api.delete(`api/keeps/${keepId}`)
    AppState.keeps = AppState.keeps.filter(k => k.id != keepId)
  }
}

export const keepsService = new KeepsService()