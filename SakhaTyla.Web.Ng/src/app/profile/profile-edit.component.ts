import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { TranslocoService } from '@ngneat/transloco';

import { Error } from '../core/error.model';
import { ProfileService } from './profile.service';
import { Profile } from './profile.model';
import { UpdateProfile } from './profile-request.model';
import { NoticeHelper } from '../core/notice.helper';

@Component({
  selector: 'app-profile-edit',
  templateUrl: './profile-edit.component.html',
  styleUrls: ['./profile-edit.component.scss']
})
export class ProfileEditComponent implements OnInit {
  profile: Profile;
  profileForm = this.fb.group({
    id: [],
    firstName: [],
    lastName: [],
  });
  error: Error;

  constructor(
    private profileService: ProfileService,
    private fb: FormBuilder,
    private noticeHelper: NoticeHelper,
    private translocoService: TranslocoService) {
  }

  ngOnInit(): void {
    this.getProfile();
  }

  private getProfile(): void {
    this.profileService.getProfile({})
      .subscribe((profile) => {
        this.profile = profile;
        this.profileForm.reset();
        this.profileForm.patchValue(this.profile);
      });
  }

  onSubmit(): void {
    this.error = null;
    this.saveProfile();
  }

  private saveProfile(): void {
    const profile: UpdateProfile = this.profileForm.value;
    this.profileService.updateProfile(profile)
      .subscribe(
        () => {
          this.noticeHelper.showMessage(this.translocoService.translate('Profile saved!'));
          this.getProfile();
        },
        (error) => {
          this.onError(error);
        }
      );
  }

  onError(error: Error) {
    this.error = error;
    if (error) {
      this.noticeHelper.showError(error);
      Error.setFormErrors(this.profileForm, error);
    }
  }

}
