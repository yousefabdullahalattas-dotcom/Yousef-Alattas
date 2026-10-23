import 'package:flutter/material.dart';

import 'button.dart';
import 'textfield.dart';

class ProfileScreen extends StatelessWidget {
  const ProfileScreen({super.key});

  @override
  Widget build(BuildContext context) {
    final profileFormKey = GlobalKey<FormState>();
    final passwordFormKey = GlobalKey<FormState>();

    return Scaffold(
      appBar: AppBar(
        title: const Text(' profile '),
        backgroundColor: Colors.deepPurple,
      ),
      body: SingleChildScrollView(
        child: Padding(
          padding: const EdgeInsets.all(20.0),
          child: Column(
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Form(
                key: profileFormKey,
                child: Column(
                  children: [
                    const MyTextField(
                      label: 'Edit User Name',
                      hint: 'Username',
                    ),
                    const SizedBox(height: 20),
                    const MyTextField(
                      label: 'Edit Email',
                      hint: 'Email',
                    ),
                    const SizedBox(height: 20),
                    MyButton(
                      text: 'Update Profile',
                      onPressed: () {
                        if (profileFormKey.currentState!.validate()) {
                        }
                      },
                    ),
                  ],
                ),
              ),

              const SizedBox(height: 20),
              const Divider(),
              const SizedBox(height: 20),

              Form(
                key: passwordFormKey,
                child: Column(
                  children: [
                    const MyTextField(
                      label: 'Enter new password',
                      hint: 'Password',
                      obscureText: true,
                    ),
                    const SizedBox(height: 20),
                    MyButton(
                      text: 'Update Password',
                      onPressed: () {
                        if (passwordFormKey.currentState!.validate()) {
                        }
                      },
                    ),
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
      bottomNavigationBar: Container(
        color: Colors.deepPurple,
        height: 80,
      ),
    );
  }
}