import { AppState } from "../AppState.js"
import { Keep } from "../models/Keep.js"
import { logger } from "../utils/Logger.js"
import { api } from "./AxiosService.js"

class VaultKeepsService {
  async getVaultKeepsByVaultId(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}/keeps`)
    AppState.keeps = res.data.map(d => new Keep(d))
  }

  async createVaultKeep(vaultKeepData) {
    vaultKeepData.keepId = AppState.activeKeep.id
    await api.post('api/vaultKeeps', vaultKeepData)
  }

  async removeVaultKeep(vaultKeepId) {
    // FIXME auto update not working
    // AppState.keeps.forEach(k => logger.log(k.vaultKeepId))
    // await api.delete(`api/vaultKeeps/${vaultKeepId}`)
    // AppState.keeps = AppState.keeps.filter(k => k.vaultKeepId == vaultKeepId)
    // logger.log(AppState.keeps)
  }
}

export const vaultKeepsService = new VaultKeepsService()