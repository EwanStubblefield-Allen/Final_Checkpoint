import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { api } from "./AxiosService.js"

class VaultsService {
  async getVaultsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    AppState.vaults = res.data.map(d => new Vault(d))
  }
}

export const vaultsService = new VaultsService()