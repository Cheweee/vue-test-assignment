<template>
  <div class="h-100 md-layout md-alignment-center-center">
    <form novalidate class="md-layout md-alignment-center-center" @submit.prevent="validateUser">
      <md-card class="md-layout-item md-size-50 md-small-size-100">
        <md-card-header>
          <div class="md-title">Регистрация</div>
        </md-card-header>

        <md-card-content>
          <div class="md-layout md-gutter">
            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('firstname')">
                <label for="first-name">Имя</label>
                <md-input
                  name="first-name"
                  id="first-name"
                  autocomplete="given-name"
                  v-model="form.firstname"
                  :disabled="sending"
                />
                <span
                  class="md-error"
                  v-if="!$v.form.firstname.required"
                >Имя пользователя обязательно</span>
                <span
                  class="md-error"
                  v-else-if="!$v.form.firstname.minlength"
                >Имя должно быть длиннее {{$v.form.firstname.minlength}} символов</span>
              </md-field>
            </div>

            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('lastname')">
                <label for="last-name">Фамилия</label>
                <md-input
                  name="last-name"
                  id="last-name"
                  autocomplete="family-name"
                  v-model="form.lastname"
                  :disabled="sending"
                />
                <span
                  class="md-error"
                  v-if="!$v.form.lastname.required"
                >Фамилия пользователя обязательна</span>
                <span
                  class="md-error"
                  v-else-if="!$v.form.lastname.minlength"
                >Фамилия должна быть длиннее {{$v.form.lastname.minlength}} символов</span>
              </md-field>
            </div>
          </div>

          <div class="md-layout md-gutter">
            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('gender')">
                <label for="gender">Пол</label>
                <md-select
                  name="gender"
                  id="gender"
                  v-model="form.gender"
                  md-dense
                  :disabled="sending"
                >
                  <md-option></md-option>
                  <md-option value="0">Мужской</md-option>
                  <md-option value="1">Женский</md-option>
                </md-select>
                <span class="md-error">Пол обязателен</span>
              </md-field>
            </div>

            <div class="md-layout-item md-small-size-100">
              <md-field :class="getValidationClass('age')">
                <label for="age">Возраст</label>
                <md-input
                  type="number"
                  id="age"
                  name="age"
                  autocomplete="age"
                  v-model="form.age"
                  :disabled="sending"
                />
                <span class="md-error" v-if="!$v.form.age.required">Возраст пользователя обязателен</span>
              </md-field>
            </div>
          </div>

          <md-field :class="getValidationClass('email')">
            <label for="email">Email</label>
            <md-input
              type="email"
              name="email"
              id="email"
              autocomplete="email"
              v-model="form.email"
              :disabled="sending"
            />
            <span class="md-error" v-if="!$v.form.email.required">Email обязателен</span>
            <span class="md-error" v-else-if="!$v.form.email.email">Невалидный email</span>
          </md-field>
        </md-card-content>

        <md-progress-bar md-mode="indeterminate" v-if="sending" />

        <md-card-actions>
          <md-button type="submit" class="md-primary" :disabled="sending">Зарегистрироваться</md-button>
        </md-card-actions>
      </md-card>

      <md-snackbar :md-active.sync="showSnackbar">{{ message }}</md-snackbar>
    </form>
  </div>
</template>

<script>
import { validationMixin } from "vuelidate";
import {
  required,
  email,
  minLength,
  maxLength
} from "vuelidate/lib/validators";

export default {
  name: "FormValidation",
  mixins: [validationMixin],
  data: () => ({
    form: {
      firstname: null,
      lastname: null,
      gender: null,
      age: null,
      email: null
    },
    showSnackbar: false,
    sending: false,
    message: null
  }),
  validations: {
    form: {
      firstname: {
        required,
        minLength: minLength(3)
      },
      lastname: {
        required,
        minLength: minLength(3)
      },
      age: {
        required,
        maxLength: maxLength(3)
      },
      gender: {
        required
      },
      email: {
        required,
        email
      }
    }
  },
  methods: {
    getValidationClass(fieldName) {
      const field = this.$v.form[fieldName];

      if (field) {
        return {
          "md-invalid": field.$invalid && field.$dirty
        };
      }
    },
    clearForm() {
      this.$v.$reset();
      this.form.firstname = null;
      this.form.lastname = null;
      this.form.age = null;
      this.form.gender = null;
      this.form.email = null;
    },
    saveUser() {
      this.sending = true;
      let soapOrRest = parseInt(this.form.gender);
      const body = JSON.stringify({
        firstname: this.form.firstname,
        lastname: this.form.lastname,
        email: this.form.email,
        gender: this.form.gender,
        age: this.form.age,
        username: "admin"
      });

      if (soapOrRest) {
        fetch("/UserService.asmx", {
          method: "POST",
          body: this.form
        })
          .then(response => {
            this.message = `Здравствуйте, ${this.form.firstname} ${this.form.lastname}! Вы успешно зарегистрировались!`;
            this.sending = false;
            this.showSnackbar = true;
            this.clearForm();
          })
          .catch(error => {
            this.message = error;
            this.showSnackbar = true;
            this.sending = false;
          });
      } else {
        fetch("/Home/CreateUser", {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: body
        })
          .then(response => {
            this.message = `Здравствуйте, ${this.form.firstname} ${this.form.lastname}! Вы успешно зарегистрировались!`;
            this.sending = false;
            this.showSnackbar = true;
            this.clearForm();
          })
          .catch(error => {
            this.message = error;
            this.showSnackbar = true;
            this.sending = false;
          });
      }
    },
    validateUser() {
      this.$v.$touch();

      if (!this.$v.$invalid) {
        this.saveUser();
      }
    }
  }
};
</script>

<style lang="scss">
.h-100 {
  height: 100%;
}
</style>