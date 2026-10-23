import 'package:flutter/material.dart';

import 'app_drawer.dart';

class HomeScreen extends StatelessWidget {
  const HomeScreen({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: const AppDrawer(),
      appBar: AppBar(
        title: const Text('Home'),
        backgroundColor: Colors.blue,
      ),
      body: Container(
        width: double.infinity,
        height: double.infinity,
        decoration: const BoxDecoration(
          gradient: RadialGradient(
            colors: [Colors.blue, Colors.black26],
            radius: 0.9,
          ),
        ),
        child: const Center(
          child: Text(
            'Home screen',
            style: TextStyle(
              fontSize: 30,
              color: Colors.white,
              fontWeight: FontWeight.w700,
            ),
          ),
        ),
      ),
      bottomNavigationBar: Container(
        color: Colors.blue,
        height: 80,
      ),
    );
  }
}