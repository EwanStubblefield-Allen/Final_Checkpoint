import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { api } from "./AxiosService.js"

class KeepsService {
  async getKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data.map(d => new Keep(d))
  }

  async getKeepsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/keeps`)
    AppState.keeps = res.data.map(d => new Keep(d))
  }
}

export const keepsService = new KeepsService()