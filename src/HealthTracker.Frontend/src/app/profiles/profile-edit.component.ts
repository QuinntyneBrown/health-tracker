import { Profile } from "./profile.model";
import { ProfileService } from "./profile.service";
import { EditorComponent } from "../shared";
import { Router } from "../router";

const template = require("./profile-edit.component.html");
const styles = require("./profile-edit.component.scss");

export class ProfileEditComponent extends HTMLElement {
    constructor(
        private _profileService: ProfileService = ProfileService.Instance,
        private _router: Router = Router.Instance
        ) {
        super();
        this.onSave = this.onSave.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onTitleClick = this.onTitleClick.bind(this);
    }

    static get observedAttributes() {
        return ["profile-id"];
    }
    
    connectedCallback() {        
        this.innerHTML = `<style>${styles}</style> ${template}`; 
        this._bind();
        this._setEventListeners();
    }
    
    private async _bind() {
        this._titleElement.textContent = this.profileId ? "Edit Profile": "Create Profile";

        if (this.profileId) {
            const profile: Profile = await this._profileService.getById(this.profileId);                
            this._nameInputElement.value = profile.name;  
        } else {
            this._deleteButtonElement.style.display = "none";
        }     
    }

    private _setEventListeners() {
        this._saveButtonElement.addEventListener("click", this.onSave);
        this._deleteButtonElement.addEventListener("click", this.onDelete);
        this._titleElement.addEventListener("click", this.onTitleClick);
    }

    private disconnectedCallback() {
        this._saveButtonElement.removeEventListener("click", this.onSave);
        this._deleteButtonElement.removeEventListener("click", this.onDelete);
        this._titleElement.removeEventListener("click", this.onTitleClick);
    }

    public async onSave() {
        const profile = {
            id: this.profileId,
            name: this._nameInputElement.value
        } as Profile;
        
        await this._profileService.add(profile);
        this._router.navigate(["profile","list"]);
    }

    public async onDelete() {        
        await this._profileService.remove({ id: this.profileId });
        this._router.navigate(["profile","list"]);
    }

    public onTitleClick() {
        this._router.navigate(["profile", "list"]);
    }

    attributeChangedCallback(name, oldValue, newValue) {
        switch (name) {
            case "profile-id":
                this.profileId = newValue;
                break;
        }        
    }

    public profileId: number;
    
    private get _titleElement(): HTMLElement { return this.querySelector("h2") as HTMLElement; }
    private get _saveButtonElement(): HTMLElement { return this.querySelector(".save-button") as HTMLElement };
    private get _deleteButtonElement(): HTMLElement { return this.querySelector(".delete-button") as HTMLElement };
    private get _nameInputElement(): HTMLInputElement { return this.querySelector(".profile-name") as HTMLInputElement;}
}

customElements.define(`ce-profile-edit`,ProfileEditComponent);
