import{_ as V,u as f,m as w,w as I,e as b,A as n,c as u,P as l,v,r as k,o as c,a as r,b as t,t as p,j as g,F as h,f as E,g as x,n as S,p as B,l as T}from"./index-0061d289.js";const K={setup(){const s=f(),a=w();I(()=>{m(s.params.vaultId),e(s.params.vaultId)}),b(()=>{n.activeVault=null,n.keeps=[]});async function m(o){try{await v.getVaultById(o)}catch(d){l.error(d.message,"[GETTING VAULT BY ID]"),a.push("/")}}async function e(o){try{await S.getVaultKeepsByVaultId(o)}catch(d){l.error(d.message,"[GETTING VAULT KEEPS BY VAULT ID]")}}return{account:u(()=>n.account),vault:u(()=>n.activeVault),keeps:u(()=>n.keeps),isEditing(){n.vaultEdit=!0},async removeVault(){try{if(!await l.confirm(`Are you sure you want to delete ${this.vault.name}?`))return;await v.removeVault(this.vault.id),a.push("/"),l.toast("Vault was deleted!")}catch(o){l.error(o.message,"[DELETING Vault]")}}}}},_=s=>(B("data-v-f5e96617"),s=s(),T(),s),P={key:0,class:"col-10 col-md-5 py-3"},A={class:"position-relative"},C=["src","alt"],D={class:"row"},N={class:"col-10 card-name text-center p-2"},G={class:"fs-1"},L={class:"pt-2"},U={key:0,class:"dropdown text-end"},F=_(()=>t("button",{type:"button",class:"btn selectable no-select mdi mdi-dots-horizontal fs-3 py-0 px-2","data-bs-toggle":"dropdown","aria-expanded":"false",title:"More Options"},null,-1)),j={class:"dropdown-menu dropdown-menu-end p-0","aria-labelledby":"vaultDropdown"},R={class:"list-group text-center"},Y=_(()=>t("p",{class:"mdi mdi-pencil"},"Edit Vault",-1)),z=[Y],M=_(()=>t("p",{class:"mdi mdi-trash-can"},"Delete Vault",-1)),O=[M],q={class:"d-flex justify-content-center"},H={class:"count p-2"},J={class:"col-12 col-md-10"},Q={class:"masonry"};function W(s,a,m,e,o,d){const y=k("KeepCard");return c(),r(h,null,[e.vault?(c(),r("div",P,[t("div",A,[t("img",{class:"rounded w-100",src:e.vault.img,alt:e.vault.name},null,8,C),t("section",D,[t("div",N,[t("p",G,p(e.vault.name),1),t("p",null,"by "+p(e.vault.creator.name),1)])])]),t("div",L,[e.vault.creator.id==e.account.id?(c(),r("div",U,[F,t("div",j,[t("div",R,[t("div",{onClick:a[0]||(a[0]=i=>e.isEditing()),class:"list-group-item dropdown-item list-group-item-action selectable","data-bs-toggle":"modal","data-bs-target":"#vaultForm"},z),t("div",{onClick:a[1]||(a[1]=i=>e.removeVault()),class:"list-group-item dropdown-item list-group-item-action text-danger selectable"},O)])])])):g("",!0),t("div",q,[t("p",H,p(e.keeps.length)+" Keeps",1)])])])):g("",!0),t("div",J,[t("div",Q,[(c(!0),r(h,null,E(e.keeps,i=>(c(),r("div",{key:i.id,class:"masonry-item my-2"},[x(y,{keepProp:i},null,8,["keepProp"])]))),128))])])],64)}const Z=V(K,[["render",W],["__scopeId","data-v-f5e96617"]]);export{Z as default};
