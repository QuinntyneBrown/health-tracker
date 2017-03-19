import { ProfileService } from "./profile.service";
import { Profile } from "./profile.model";

const template = require("./my-profile.component.html");
const styles = require("./my-profile.component.scss");

export class MyProfileComponent extends HTMLElement {
    constructor(private _profileService: ProfileService) {
        super();
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
        //this.myProfile = await this._profileService.getMyProfile();

    }

    private _setEventListeners() {

    }

    disconnectedCallback() {

    }

    attributeChangedCallback (name, oldValue, newValue) {
        switch (name) {
            default:
                break;
        }
    }
}

customElements.define(`ce-my-profile`,MyProfileComponent);
