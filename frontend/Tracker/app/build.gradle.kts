// import com.android.kotlin.multiplatform.models.AndroidSourceSet
import com.google.protobuf.gradle.*

plugins {
    alias(libs.plugins.androidApplication)
    alias(libs.plugins.jetbrainsKotlinAndroid)

    // KSP @ ROOM database
    id("com.google.devtools.ksp") version "1.9.21-1.0.15"

    // gRPC
    id("idea")
    id("com.google.protobuf") version "0.9.4"
}

android {
    namespace = "eu.petsontrail.tracker"
    compileSdk = 34

    defaultConfig {
        applicationId = "eu.petsontrail.tracker"
        minSdk = 24
        targetSdk = 34
        versionCode = 1
        versionName = "1.0"

        testInstrumentationRunner = "androidx.test.runner.AndroidJUnitRunner"

        // @ ROOM database
        ksp {
            arg("room.schemaLocation", "$projectDir/schemas")
        }

        manifestPlaceholders["appAuthRedirectScheme"] = "net.openid.petsontrail"
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
    kotlinOptions {
        jvmTarget = "1.8"
    }
    buildFeatures {
        viewBinding = true
    }
}

dependencies {
    implementation(libs.androidx.core.ktx)
    implementation(libs.androidx.appcompat)
    implementation(libs.material)
    implementation(libs.androidx.constraintlayout)
    implementation(libs.androidx.navigation.fragment.ktx)
    implementation(libs.androidx.navigation.ui.ktx)
    implementation(libs.androidx.room.common)
    implementation(libs.androidx.room.ktx)
    implementation(libs.play.services.location)
    implementation(libs.androidx.annotation)
    implementation(libs.androidx.lifecycle.livedata.ktx)
    implementation(libs.androidx.lifecycle.viewmodel.ktx)
    testImplementation(libs.junit)
    androidTestImplementation(libs.androidx.junit)
    androidTestImplementation(libs.androidx.espresso.core)
    implementation(libs.material.v1130alpha02)

    // ROOM database implementation
    implementation(libs.androidx.room.runtime)
    annotationProcessor(libs.androidx.room.compiler)
    implementation(libs.androidx.room.ktx)//KTX Extensions/Coroutines for Room
    ksp(libs.androidx.room.compiler)

    implementation(libs.grpc.okhttp)
    implementation(libs.grpc.protobuf.lite)
    implementation(libs.grpc.stub)
    implementation(libs.grpc.auth)
    implementation(libs.annotations.api)

    // OAuth authentication
    implementation(libs.appauth)
    implementation(libs.okhttp)

    protobuf(files("../../../Protos/"))
}

protobuf {
    protoc {
        artifact = "com.google.protobuf:protoc:4.26.1"
    }

    plugins {
        id("grpc") {
            artifact = "io.grpc:protoc-gen-grpc-java:1.63.0"
        }
    }

    generateProtoTasks {
        all().forEach {
            it.builtins {
                id("java") { option("lite") }
            }
            it.plugins {
                id("grpc") { option("lite") }
            }
        }
    }
}
