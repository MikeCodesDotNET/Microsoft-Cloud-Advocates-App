//
//  SecondViewController.swift
//  Advocates
//
//  Created by Michael James on 11/06/2019.
//  Copyright © 2019 Mike James. All rights reserved.
//

import UIKit

import AppCenter
import AppCenterAuth

import SkeletonView


class AccountViewController: UIViewController {

    override func viewDidLoad() {
        super.viewDidLoad()
        // Do any additional setup after loading the view.
        setupUI()
    }

    override func viewDidAppear(_ animated: Bool) {
        
        if accountViewModel.signIn() == true {
            //Ensure we update the UI on the main thread.
            self.fullNameLabel.text = self.accountViewModel.fullName
            self.displayNameLabel.text = self.accountViewModel.displayName
            self.avatarImageView.image = self.accountViewModel.avatarImage
            
            
        }
    }
    
    private func setupUI(){
        
        //Loading Animations
        let gradient = SkeletonGradient(baseColor: UIColor.clouds)
        
        let animation = SkeletonAnimationBuilder().makeSlidingAnimation(withDirection: .topLeftBottomRight)
        
        //Setup Avatar Image
        avatarImageView.isSkeletonable = true
        avatarImageView.showAnimatedGradientSkeleton(usingGradient: gradient, animation: animation)
        
        avatarImageView.contentMode = .scaleAspectFit
        avatarImageView.layer.cornerRadius = avatarImageView.frame.width / 2
        avatarImageView.layer.masksToBounds = true
        
        //Labels
        fullNameLabel.isSkeletonable = true
        fullNameLabel.linesCornerRadius = 10
        fullNameLabel.showAnimatedGradientSkeleton(usingGradient: gradient, animation: animation)
        
        displayNameLabel.isSkeletonable = true
        displayNameLabel.linesCornerRadius = 10
        displayNameLabel.showAnimatedGradientSkeleton(usingGradient: gradient, animation: animation)
        
    }
    
    //Services
    let accountViewModel: AccountViewModel = AccountViewModel.init()
    
    //Outlets
    @IBOutlet var avatarImageView: UIImageView!
    @IBOutlet var fullNameLabel: UILabel!
    @IBOutlet var displayNameLabel: UILabel!
    

}

