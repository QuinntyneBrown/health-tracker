import { ProfileService } from "./profile.service";
import { Profile } from "./profile.model";

const template = require("./my-profile.component.html");
const styles = require("./my-profile.component.scss");

export class MyProfileComponent extends HTMLElement {
    constructor(private _profileService: ProfileService) {
        super();
        this.onSave = this.onSave.bind(this);
    }

    static get observedAttributes () {
        return [];
    }

    public myProfile: Profile = <Profile>{};

    connectedCallback() {
        this.innerHTML = `<style>${styles}</style> ${template}`;
        this._bind();
        this._setEventListeners();
    }

    private async _bind() {
        this.myProfile = await this._profileService.getMyProfile();
        this.masterDetailElement.setAttribute("weight-snap-shots", JSON.stringify(this.myProfile.weightSnapShots));
    }

    private _setEventListeners() {
        this.saveButtonElement.addEventListener("click", this.onSave);
    }

    public async onSave() {        
        this.myProfile.weightSnapShots = this.masterDetailElement.value as any;
        await this._profileService.add(this.myProfile);
        this._bind();
    }
    
    public get saveButtonElement(): HTMLElement { return this.querySelector("ce-button") as HTMLElement; }
    public get masterDetailElement(): HTMLInputElement { return this.querySelector("ce-weight-snap-shot-master-detail-embed") as HTMLInputElement; }
}

customElements.define(`ce-my-profile`,MyProfileComponent);
