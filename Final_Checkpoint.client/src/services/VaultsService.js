import { AppState } from "../AppState.js"
import { Vault } from "../models/Vault.js"
import { api } from "./AxiosService.js"
import Pop from "../utils/Pop.js"

class VaultsService {
  async getVaultById(vaultId) {
    const res = await api.get(`api/vaults/${vaultId}`)
    AppState.activeVault = new Vault(res.data)
  }

  async getVaultsByAccountId() {
    try {
      const res = await api.get('account/vaults')
      AppState.myVaults = res.data.map(d => new Vault(d))
    } catch (error) {
      Pop.error(error.message, '[GETTING MY VAULTS]')
    }
  }

  async getVaultsByProfileId(profileId) {
    const res = await api.get(`api/profiles/${profileId}/vaults`)
    AppState.vaults = res.data.map(d => new Vault(d))
  }

  async createVault(vaultData) {
    const res = await api.post('api/vaults', vaultData)
    const vault = new Vault(res.data)

    if (AppState.account.id == AppState.profile.id) {
      AppState.vaults.push(vault)
    }
    AppState.myVaults.push(vault)
    return vault.id
  }

  async updateVault(vaultData) {
    const res = await api.put(`api/vaults/${vaultData.id}`, vaultData)
    const vault = new Vault(res.data)
    AppState.activeVault = vault
    const foundIndex = AppState.myVaults.findIndex(v => v.id == vault.id)
    AppState.myVaults.splice(foundIndex, 1, vault)
  }

  async removeVault(vaultId) {
    await api.delete(`api/vaults/${vaultId}`)
    AppState.vaults, AppState.myVaults = AppState.vaults.filter(v => v.id != vaultId)
  }
}

export const vaultsService = new VaultsService()