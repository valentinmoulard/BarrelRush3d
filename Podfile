source 'https://cdn.cocoapods.org/'
platform :ios, '13'


target 'UnityFramework' do
  pod 'FBSDKCoreKit', '~> 15.1'
  pod 'FBSDKCoreKit_Basics', '~> 15.1'
  pod 'FBSDKGamingServicesKit', '~> 15.1'
  pod 'FBSDKLoginKit', '~> 15.1'
  pod 'FBSDKShareKit', '~> 15.1'
  pod 'StackConsentManager'
  pod 'AppsFlyerFramework'
  pod 'Appodeal'
  # Add the Firebase pod for Google Analytics
  pod 'Firebase/Analytics'
  # Add the Firebase Cloud Messaging pod
  pod 'Firebase/Messaging'
end
target 'Unity-iPhone' do
end
use_frameworks!

post_install do |installer|
      installer.pods_project.targets.each do |target|
          target.build_configurations.each do |config|
          xcconfig_path = config.base_configuration_reference.real_path
          xcconfig = File.read(xcconfig_path)
          xcconfig_mod = xcconfig.gsub(/DT_TOOLCHAIN_DIR/, "TOOLCHAIN_DIR")
          File.open(xcconfig_path, "w") { |file| file << xcconfig_mod }
          end
      end
  end