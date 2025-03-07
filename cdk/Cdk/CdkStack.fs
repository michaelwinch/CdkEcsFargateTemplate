namespace CdkStack

open Amazon.CDK
open Amazon.CDK.AWS.ECR
open Amazon.CDK.AWS.ECS

type CdkStack(scope, id, props) as this =
    inherit Stack(scope, id, props)

    let ecrRepository =
        let props = RepositoryProps (RepositoryName = "cdk_ecs_fargate_app")
        Repository (this, "cdk_ecs_fargate_app-ecr", props)

    let containerImage = ContainerImage.FromEcrRepository ecrRepository

    let taskDefinition =
        let props = FargateTaskDefinitionProps (Family = "cdk_ecs_fargate_app-td")
        FargateTaskDefinition (this, "cdk_ecs_fargate_app-td", props)

    let containerDefinition =
        let props =
            ContainerDefinitionProps (
                Image = containerImage,
                TaskDefinition = taskDefinition,
                Logging = AwsLogDriver (AwsLogDriverProps (StreamPrefix = "ecs"))
            )

        ContainerDefinition (this, "cdk_ecs_fargate_app-cd", props)
