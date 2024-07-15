plugins {
    id("com.android.library")
}

android {
    namespace = "com.example.charts"
    compileSdk = 34

    defaultConfig {
        minSdk = 21
    }

    buildTypes {
        release {
            isMinifyEnabled = false
            proguardFiles(
                getDefaultProguardFile("proguard-android-optimize.txt"),
                "proguard-rules.pro"
            )
        }
    }
    compileOptions {
        sourceCompatibility = JavaVersion.VERSION_1_8
        targetCompatibility = JavaVersion.VERSION_1_8
    }
}

// Create configuration for copyDependencies. Uncomment line below.
configurations {
    create("copyDependencies")
}

dependencies {

    // Add package dependency for binding library. Uncomment line below and add your dependency.
    implementation("com.github.PhilJay:MPAndroidChart:v3.1.0")

    // Copy dependencies for binding library. Uncomment line below and add your dependency.
     "copyDependencies"("com.github.PhilJay:MPAndroidChart:v3.1.0")
}

// Copy dependencies for binding library. Uncomment code blocks below.
project.afterEvaluate {
    tasks.register<Copy>("copyDeps") {
        from(configurations["copyDependencies"])
        into("${projectDir}/build/outputs/deps")
    }
    tasks.named("preBuild") { finalizedBy("copyDeps") }
}
